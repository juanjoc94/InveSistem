// TEMPORAL: lista en memoria. Lo reemplaza Juanjo por la base de datos.
using API.Modelos;

namespace API.Datos;

public class RepositorioProductoMemoria : IRepositorioProducto
{
    private readonly List<Producto> _lista = new();
    private int _siguienteId = 1;

    public IEnumerable<Producto> ObtenerTodos() => _lista.ToList();

    public Producto? ObtenerPorId(int id) => _lista.FirstOrDefault(x => x.Id == id);

    public Producto Crear(Producto entidad)
    {
        entidad.Id = _siguienteId++;
        _lista.Add(entidad);
        return entidad;
    }

    public void Actualizar(Producto entidad) { /* el objeto ya está en la lista */ }

    public bool Eliminar(int id) => _lista.RemoveAll(x => x.Id == id) > 0;
}
