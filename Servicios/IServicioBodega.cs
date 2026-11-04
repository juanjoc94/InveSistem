using Modelos.DTOs;

namespace Servicios;

public interface IServicioBodega
{
    IEnumerable<BodegaDto> ObtenerTodos();
    BodegaDto? ObtenerPorId(int id);
    BodegaDto Crear(BodegaGuardarDto dto);
    BodegaDto? Actualizar(int id, BodegaGuardarDto dto);
    bool Eliminar(int id);
}
