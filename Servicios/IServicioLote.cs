using Modelos.DTOs;

namespace Servicios;

public interface IServicioLote
{
    IEnumerable<LoteDto> ObtenerTodos();
    LoteDto? ObtenerPorId(int id);
    LoteDto Crear(LoteGuardarDto dto);
    LoteDto? Actualizar(int id, LoteGuardarDto dto);
    bool Eliminar(int id);
}
