using System.ComponentModel.DataAnnotations;
using API.Datos;
using API.Modelos;
using API.Modelos.DTOs;
using API.Servicios;
using Xunit;

namespace Pruebas;

public class ServicioLoteTests
{
    // Crea el servicio con un producto (id 1) y una bodega (id 1) ya existentes
    private static ServicioLote CrearServicio()
    {
        var repoProducto = new RepositorioProductoMemoria();
        var repoBodega = new RepositorioBodegaMemoria();
        repoProducto.Crear(new Producto { Nombre = "Arroz", Precio = 100 });
        repoBodega.Crear(new Bodega { Nombre = "Central", Ubicacion = "Medellín", Capacidad = 100 });

        return new ServicioLote(new RepositorioLoteMemoria(), repoProducto, repoBodega);
    }

    private static LoteGuardarDto DtoValido() => new()
    {
        ProductoId = 1,
        BodegaId = 1,
        Cantidad = 50,
        FechaVencimiento = new DateTime(2027, 1, 1)
    };

    [Fact]
    public void Crear_DatosValidos_DevuelveLoteConId()
    {
        var creado = CrearServicio().Crear(DtoValido());

        Assert.True(creado.Id > 0);
        Assert.Equal(50, creado.Cantidad);
    }

    [Fact]
    public void Crear_ProductoInexistente_LanzaArgumentException()
    {
        var servicio = CrearServicio();
        var dto = DtoValido();
        dto.ProductoId = 99;

        Assert.Throws<ArgumentException>(() => servicio.Crear(dto));
    }

    [Fact]
    public void Crear_BodegaInexistente_LanzaArgumentException()
    {
        var servicio = CrearServicio();
        var dto = DtoValido();
        dto.BodegaId = 99;

        Assert.Throws<ArgumentException>(() => servicio.Crear(dto));
    }

    [Fact]
    public void Crear_CantidadCero_FallaValidacion()
    {
        var dto = DtoValido();
        dto.Cantidad = 0;
        var errores = new List<ValidationResult>();

        Validator.TryValidateObject(dto, new ValidationContext(dto), errores, true);

        Assert.Contains(errores, e => e.MemberNames.Contains("Cantidad"));
    }

    [Fact]
    public void ObtenerPorId_Inexistente_DevuelveNull()
    {
        Assert.Null(CrearServicio().ObtenerPorId(999));
    }

    [Fact]
    public void Actualizar_Inexistente_DevuelveNull()
    {
        Assert.Null(CrearServicio().Actualizar(999, DtoValido()));
    }

    [Fact]
    public void Actualizar_Existente_CambiaLaCantidad()
    {
        var servicio = CrearServicio();
        var creado = servicio.Crear(DtoValido());
        var cambios = DtoValido();
        cambios.Cantidad = 80;

        var actualizado = servicio.Actualizar(creado.Id, cambios);

        Assert.Equal(80, actualizado!.Cantidad);
    }

    [Fact]
    public void Eliminar_Existente_DevuelveTrue_Y_Inexistente_DevuelveFalse()
    {
        var servicio = CrearServicio();
        var creado = servicio.Crear(DtoValido());

        Assert.True(servicio.Eliminar(creado.Id));
        Assert.False(servicio.Eliminar(creado.Id));
    }
}
