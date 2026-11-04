using API.Datos;
using API.Modelos;
using API.Modelos.DTOs;

namespace API.Servicios;

public class ServicioLote : IServicioLote
{
    private readonly IRepositorioLote _repo;
    private readonly IRepositorioProducto _repoProducto;
    private readonly IRepositorioBodega _repoBodega;

    public ServicioLote(IRepositorioLote repo, IRepositorioProducto repoProducto, IRepositorioBodega repoBodega)
    {
        _repo = repo;
        _repoProducto = repoProducto;
        _repoBodega = repoBodega;
    }

    public IEnumerable<LoteDto> ObtenerTodos() => _repo.ObtenerTodos().Select(ADto);

    public LoteDto? ObtenerPorId(int id)
    {
        var l = _repo.ObtenerPorId(id);
        return l is null ? null : ADto(l);
    }

    // Lanza ArgumentException si el producto o la bodega no existen (el controlador lo vuelve 400)
    public LoteDto Crear(LoteGuardarDto dto)
    {
        ValidarReferencias(dto);
        var nuevo = new Lote
        {
            ProductoId = dto.ProductoId,
            BodegaId = dto.BodegaId,
            Cantidad = dto.Cantidad,
            FechaVencimiento = dto.FechaVencimiento
        };
        return ADto(_repo.Crear(nuevo));
    }

    public LoteDto? Actualizar(int id, LoteGuardarDto dto)
    {
        var existente = _repo.ObtenerPorId(id);
        if (existente is null) return null;

        ValidarReferencias(dto);

        existente.ProductoId = dto.ProductoId;
        existente.BodegaId = dto.BodegaId;
        existente.Cantidad = dto.Cantidad;
        existente.FechaVencimiento = dto.FechaVencimiento;

        _repo.Actualizar(existente);
        return ADto(existente);
    }

    public bool Eliminar(int id)
    {
        if (_repo.ObtenerPorId(id) is null) return false;
        return _repo.Eliminar(id);
    }

    private void ValidarReferencias(LoteGuardarDto dto)
    {
        if (_repoProducto.ObtenerPorId(dto.ProductoId) is null)
            throw new ArgumentException("El producto no existe");
        if (_repoBodega.ObtenerPorId(dto.BodegaId) is null)
            throw new ArgumentException("La bodega no existe");
    }

    private static LoteDto ADto(Lote l) => new()
    {
        Id = l.Id,
        ProductoId = l.ProductoId,
        BodegaId = l.BodegaId,
        Cantidad = l.Cantidad,
        FechaVencimiento = l.FechaVencimiento
    };
}
