// TEMPORAL: esto lo reemplaza lo que suba Juanjo en la carpeta Datos.
using API.Modelos;

namespace API.Datos;

public interface IRepositorioProducto
{
    IEnumerable<Producto> ObtenerTodos();
    Producto? ObtenerPorId(int id);
    Producto Crear(Producto entidad);
    void Actualizar(Producto entidad);
    bool Eliminar(int id);
}
