using API.Datos;
using API.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IServicioProducto, ServicioProducto>();
builder.Services.AddScoped<IServicioLote, ServicioLote>();
builder.Services.AddScoped<IServicioBodega, ServicioBodega>();

// Repositorios en memoria (Juanjo los reemplaza por la base de datos)
builder.Services.AddSingleton<IRepositorioProducto, RepositorioProductoMemoria>();
builder.Services.AddSingleton<IRepositorioLote, RepositorioLoteMemoria>();
builder.Services.AddSingleton<IRepositorioBodega, RepositorioBodegaMemoria>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(o => o.SwaggerEndpoint("/openapi/v1.json", "API"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();