using System.ComponentModel.DataAnnotations;
using API.Datos;
using API.Modelos.DTOs;
using API.Servicios;
using Xunit;

namespace Pruebas;

public class ServicioProductoTests
{
    private static ServicioProducto CrearServicio() =>
        new ServicioProducto(new RepositorioProductoMemoria());

    private static ProductoGuardarDto DtoValido(string nombre = "Arroz") => new()
    {
        Nombre = nombre,
        Descripcion = "Bulto 50kg",
        Precio = 120000
    };

    private static List<ValidationResult> Validar(object dto)
    {
        var resultados = new List<ValidationResult>();
        Validator.TryValidateObject(dto, new ValidationContext(dto), resultados, true);
        return resultados;
    }

    [Fact]
    public void Crear_DatosValidos_DevuelveProductoConId()
    {
        var servicio = CrearServicio();

        var creado = servicio.Crear(DtoValido());

        Assert.True(creado.Id > 0);
        Assert.Equal("Arroz", creado.Nombre);
        Assert.Equal(120000, creado.Precio);
    }

    [Fact]
    public void Crear_DosProductos_TienenIdsDistintos()
    {
        var servicio = CrearServicio();

        var a = servicio.Crear(DtoValido("A"));
        var b = servicio.Crear(DtoValido("B"));

        Assert.NotEqual(a.Id, b.Id);
    }

    [Fact]
    public void Crear_NombreVacio_FallaValidacion()
    {
        var dto = DtoValido("");

        var errores = Validar(dto);

        Assert.Contains(errores, e => e.MemberNames.Contains("Nombre"));
    }

    [Fact]
    public void Crear_PrecioCero_FallaValidacion()
    {
        var dto = DtoValido();
        dto.Precio = 0;

        var errores = Validar(dto);

        Assert.Contains(errores, e => e.MemberNames.Contains("Precio"));
    }

    [Fact]
    public void Crear_DatosValidos_PasaValidacion()
    {
        Assert.Empty(Validar(DtoValido()));
    }

    [Fact]
    public void ObtenerPorId_Existente_DevuelveProducto()
    {
        var servicio = CrearServicio();
        var creado = servicio.Crear(DtoValido());

        var encontrado = servicio.ObtenerPorId(creado.Id);

        Assert.NotNull(encontrado);
        Assert.Equal(creado.Id, encontrado!.Id);
    }

    [Fact]
    public void ObtenerPorId_Inexistente_DevuelveNull()
    {
        var servicio = CrearServicio();

        Assert.Null(servicio.ObtenerPorId(999));
    }

    [Fact]
    public void ObtenerTodos_DevuelveTodosLosCreados()
    {
        var servicio = CrearServicio();
        servicio.Crear(DtoValido("A"));
        servicio.Crear(DtoValido("B"));

        Assert.Equal(2, servicio.ObtenerTodos().Count());
    }

    [Fact]
    public void Actualizar_Existente_CambiaLosDatos()
    {
        var servicio = CrearServicio();
        var creado = servicio.Crear(DtoValido());

        var actualizado = servicio.Actualizar(creado.Id, DtoValido("Arroz Premium"));

        Assert.NotNull(actualizado);
        Assert.Equal("Arroz Premium", actualizado!.Nombre);
        Assert.Equal("Arroz Premium", servicio.ObtenerPorId(creado.Id)!.Nombre);
    }

    [Fact]
    public void Actualizar_Inexistente_DevuelveNull()
    {
        var servicio = CrearServicio();

        Assert.Null(servicio.Actualizar(999, DtoValido()));
    }

    [Fact]
    public void Eliminar_Existente_DevuelveTrueYYaNoExiste()
    {
        var servicio = CrearServicio();
        var creado = servicio.Crear(DtoValido());

        var eliminado = servicio.Eliminar(creado.Id);

        Assert.True(eliminado);
        Assert.Null(servicio.ObtenerPorId(creado.Id));
    }

    [Fact]
    public void Eliminar_Inexistente_DevuelveFalse()
    {
        var servicio = CrearServicio();

        Assert.False(servicio.Eliminar(999));
    }
}
