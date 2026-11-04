// TEMPORAL: lista en memoria. Lo reemplaza Juanjo por la base de datos.
using API.Modelos;

namespace API.Datos;

public class RepositorioBodegaMemoria : IRepositorioBodega
{
    private readonly List<Bodega> _lista = new();
    private int _siguienteId = 1;

    public IEnumerable<Bodega> ObtenerTodos() => _lista.ToList();

    public Bodega? ObtenerPorId(int id) => _lista.FirstOrDefault(x => x.Id == id);

    public Bodega Crear(Bodega entidad)
    {
        entidad.Id = _siguienteId++;
        _lista.Add(entidad);
        return entidad;
    }

    public void Actualizar(Bodega entidad) { /* el objeto ya está en la lista */ }

    public bool Eliminar(int id) => _lista.RemoveAll(x => x.Id == id) > 0;
}
