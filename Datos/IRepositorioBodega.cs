// TEMPORAL: esto lo reemplaza lo que suba Juanjo en la carpeta Datos.
using Modelos;

namespace Datos;

public interface IRepositorioBodega
{
    IEnumerable<Bodega> ObtenerTodos();
    Bodega? ObtenerPorId(int id);
    Bodega Crear(Bodega entidad);
    void Actualizar(Bodega entidad);
    bool Eliminar(int id);
}
