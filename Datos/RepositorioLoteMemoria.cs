// TEMPORAL: lista en memoria. Lo reemplaza Juanjo por la base de datos.
using Modelos;

namespace Datos;

public class RepositorioLoteMemoria : IRepositorioLote
{
    private readonly List<Lote> _lista = new();
    private int _siguienteId = 1;

    public IEnumerable<Lote> ObtenerTodos() => _lista.ToList();

    public Lote? ObtenerPorId(int id) => _lista.FirstOrDefault(x => x.Id == id);

    public Lote Crear(Lote entidad)
    {
        entidad.Id = _siguienteId++;
        _lista.Add(entidad);
        return entidad;
    }

    public void Actualizar(Lote entidad) { /* el objeto ya está en la lista */ }

    public bool Eliminar(int id) => _lista.RemoveAll(x => x.Id == id) > 0;
}
