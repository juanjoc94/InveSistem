using System.ComponentModel.DataAnnotations;
using API.Datos;
using API.Modelos.DTOs;
using API.Servicios;
using Xunit;

namespace Pruebas;

public class ServicioBodegaTests
{
    private static ServicioBodega CrearServicio() =>
        new ServicioBodega(new RepositorioBodegaMemoria());

    private static BodegaGuardarDto DtoValido() => new()
    {
        Nombre = "Bodega Central",
        Ubicacion = "Medellín",
        Capacidad = 500
    };

    [Fact]
    public void Crear_DatosValidos_DevuelveBodegaConId()
    {
        var creada = CrearServicio().Crear(DtoValido());

        Assert.True(creada.Id > 0);
        Assert.Equal("Bodega Central", creada.Nombre);
    }

    [Fact]
    public void Crear_CapacidadCero_FallaValidacion()
    {
        var dto = DtoValido();
        dto.Capacidad = 0;
        var errores = new List<ValidationResult>();

        Validator.TryValidateObject(dto, new ValidationContext(dto), errores, true);

        Assert.Contains(errores, e => e.MemberNames.Contains("Capacidad"));
    }

    [Fact]
    public void ObtenerPorId_Inexistente_DevuelveNull()
    {
        Assert.Null(CrearServicio().ObtenerPorId(999));
    }

    [Fact]
    public void Actualizar_Existente_CambiaLosDatos()
    {
        var servicio = CrearServicio();
        var creada = servicio.Crear(DtoValido());
        var cambios = DtoValido();
        cambios.Capacidad = 800;

        var actualizada = servicio.Actualizar(creada.Id, cambios);

        Assert.Equal(800, actualizada!.Capacidad);
    }

    [Fact]
    public void Eliminar_Existente_DevuelveTrue()
    {
        var servicio = CrearServicio();
        var creada = servicio.Crear(DtoValido());

        Assert.True(servicio.Eliminar(creada.Id));
        Assert.Null(servicio.ObtenerPorId(creada.Id));
    }
}
