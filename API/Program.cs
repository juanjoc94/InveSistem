using Datos;
using Servicios;
using API.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<JwtService>();

builder.Services.AddScoped<IServicioProducto, ServicioProducto>();
builder.Services.AddScoped<IServicioLote, ServicioLote>();
builder.Services.AddScoped<IServicioBodega, ServicioBodega>();

builder.Services.AddSingleton<IRepositorioProducto, RepositorioProductoMemoria>();
builder.Services.AddSingleton<IRepositorioLote, RepositorioLoteMemoria>();
builder.Services.AddSingleton<IRepositorioBodega, RepositorioBodegaMemoria>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opciones =>
    {
        opciones.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(o => o.SwaggerEndpoint("/openapi/v1.json", "API"));
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
