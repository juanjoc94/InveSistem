using Modelos.DTOs;
using Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "DirectorTecnico,DirectorOperativo,JefeDePlanta")]
public class ProductoController : ControllerBase
{
    private readonly IServicioProducto _servicio;

    public ProductoController(IServicioProducto servicio) => _servicio = servicio;

    [HttpGet]
    public ActionResult<IEnumerable<ProductoDto>> Get() => Ok(_servicio.ObtenerTodos());

    [HttpGet("{id:int}")]
    public ActionResult<ProductoDto> Get(int id)
    {
        var item = _servicio.ObtenerPorId(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    [Authorize(Roles = "DirectorTecnico,DirectorOperativo")]
    public ActionResult<ProductoDto> Post(ProductoGuardarDto dto)
    {
        var creado = _servicio.Crear(dto);
        return CreatedAtAction(nameof(Get), new { id = creado.Id }, creado);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "DirectorTecnico,DirectorOperativo")]
    public ActionResult<ProductoDto> Put(int id, ProductoGuardarDto dto)
    {
        var actualizado = _servicio.Actualizar(id, dto);
        return actualizado is null ? NotFound() : Ok(actualizado);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "DirectorTecnico,DirectorOperativo")]
    public IActionResult Delete(int id) =>
        _servicio.Eliminar(id) ? NoContent() : NotFound();
}
