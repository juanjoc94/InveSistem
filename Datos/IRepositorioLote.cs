// TEMPORAL: esto lo reemplaza lo que suba Juanjo en la carpeta Datos.
using API.Modelos;

namespace API.Datos;

public interface IRepositorioLote
{
    IEnumerable<Lote> ObtenerTodos();
    Lote? ObtenerPorId(int id);
    Lote Crear(Lote entidad);
    void Actualizar(Lote entidad);
    bool Eliminar(int id);
}
