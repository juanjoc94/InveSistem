using API.Modelos.DTOs;

namespace API.Servicios;

public interface IServicioLote
{
    IEnumerable<LoteDto> ObtenerTodos();
    LoteDto? ObtenerPorId(int id);
    LoteDto Crear(LoteGuardarDto dto);
    LoteDto? Actualizar(int id, LoteGuardarDto dto);
    bool Eliminar(int id);
}
