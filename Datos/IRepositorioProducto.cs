// TEMPORAL: esto lo reemplaza lo que suba Juanjo en la carpeta Datos.
using Modelos;

namespace Datos;

public interface IRepositorioProducto
{
    IEnumerable<Producto> ObtenerTodos();
    Producto? ObtenerPorId(int id);
    Producto Crear(Producto entidad);
    void Actualizar(Producto entidad);
    bool Eliminar(int id);
}
