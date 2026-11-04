using Datos;
using Modelos;
using Modelos.DTOs;

namespace Servicios;

public class ServicioBodega : IServicioBodega
{
    private readonly IRepositorioBodega _repo;

    public ServicioBodega(IRepositorioBodega repo) => _repo = repo;

    public IEnumerable<BodegaDto> ObtenerTodos() => _repo.ObtenerTodos().Select(ADto);

    public BodegaDto? ObtenerPorId(int id)
    {
        var b = _repo.ObtenerPorId(id);
        return b is null ? null : ADto(b);
    }

    public BodegaDto Crear(BodegaGuardarDto dto)
    {
        var nueva = new Bodega
        {
            Nombre = dto.Nombre,
            Ubicacion = dto.Ubicacion,
            Capacidad = dto.Capacidad
        };
        return ADto(_repo.Crear(nueva));
    }

    public BodegaDto? Actualizar(int id, BodegaGuardarDto dto)
    {
        var existente = _repo.ObtenerPorId(id);
        if (existente is null) return null;

        existente.Nombre = dto.Nombre;
        existente.Ubicacion = dto.Ubicacion;
        existente.Capacidad = dto.Capacidad;

        _repo.Actualizar(existente);
        return ADto(existente);
    }

    public bool Eliminar(int id)
    {
        if (_repo.ObtenerPorId(id) is null) return false;
        return _repo.Eliminar(id);
    }

    private static BodegaDto ADto(Bodega b) => new()
    {
        Id = b.Id,
        Nombre = b.Nombre,
        Ubicacion = b.Ubicacion,
        Capacidad = b.Capacidad
    };
}
