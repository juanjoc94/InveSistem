using API.Datos;
using API.Modelos;
using API.Modelos.DTOs;

namespace API.Servicios;

public class ServicioProducto : IServicioProducto
{
    private readonly IRepositorioProducto _repo;

    public ServicioProducto(IRepositorioProducto repo) => _repo = repo;

    public IEnumerable<ProductoDto> ObtenerTodos() => _repo.ObtenerTodos().Select(ADto);

    public ProductoDto? ObtenerPorId(int id)
    {
        var p = _repo.ObtenerPorId(id);
        return p is null ? null : ADto(p);
    }

    public ProductoDto Crear(ProductoGuardarDto dto)
    {
        var nuevo = new Producto
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Precio = dto.Precio
        };
        return ADto(_repo.Crear(nuevo));
    }

    public ProductoDto? Actualizar(int id, ProductoGuardarDto dto)
    {
        var existente = _repo.ObtenerPorId(id);
        if (existente is null) return null;

        existente.Nombre = dto.Nombre;
        existente.Descripcion = dto.Descripcion;
        existente.Precio = dto.Precio;

        _repo.Actualizar(existente);
        return ADto(existente);
    }

    public bool Eliminar(int id)
    {
        if (_repo.ObtenerPorId(id) is null) return false;
        return _repo.Eliminar(id);
    }

    private static ProductoDto ADto(Producto p) => new()
    {
        Id = p.Id,
        Nombre = p.Nombre,
        Descripcion = p.Descripcion,
        Precio = p.Precio
    };
}
