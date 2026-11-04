using Datos;
using Servicios;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// ---- Servicios (tu parte) ----
builder.Services.AddScoped<IServicioProducto, ServicioProducto>();
builder.Services.AddScoped<IServicioLote, ServicioLote>();
builder.Services.AddScoped<IServicioBodega, ServicioBodega>();

// ---- Repositorios TEMPORALES en memoria (los reemplaza Juanjo) ----
// Singleton para que los datos no se pierdan entre peticiones.
builder.Services.AddSingleton<IRepositorioProducto, RepositorioProductoMemoria>();
builder.Services.AddSingleton<IRepositorioLote, RepositorioLoteMemoria>();
builder.Services.AddSingleton<IRepositorioBodega, RepositorioBodegaMemoria>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(o => o.SwaggerEndpoint("/openapi/v1.json", "API"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
