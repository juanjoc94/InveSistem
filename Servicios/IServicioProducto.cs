using API.Modelos.DTOs;

namespace API.Servicios;

public interface IServicioProducto
{
    IEnumerable<ProductoDto> ObtenerTodos();
    ProductoDto? ObtenerPorId(int id);
    ProductoDto Crear(ProductoGuardarDto dto);
    ProductoDto? Actualizar(int id, ProductoGuardarDto dto);
    bool Eliminar(int id);
}
