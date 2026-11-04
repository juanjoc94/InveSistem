using API.Modelos.DTOs;
using API.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BodegaController : ControllerBase
{
    private readonly IServicioBodega _servicio;

    public BodegaController(IServicioBodega servicio) => _servicio = servicio;

    [HttpGet]
    public ActionResult<IEnumerable<BodegaDto>> Get() => Ok(_servicio.ObtenerTodos());

    [HttpGet("{id:int}")]
    public ActionResult<BodegaDto> Get(int id)
    {
        var item = _servicio.ObtenerPorId(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public ActionResult<BodegaDto> Post(BodegaGuardarDto dto)
    {
        var creado = _servicio.Crear(dto);
        return CreatedAtAction(nameof(Get), new { id = creado.Id }, creado);
    }

    [HttpPut("{id:int}")]
    public ActionResult<BodegaDto> Put(int id, BodegaGuardarDto dto)
    {
        var actualizado = _servicio.Actualizar(id, dto);
        return actualizado is null ? NotFound() : Ok(actualizado);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id) =>
        _servicio.Eliminar(id) ? NoContent() : NotFound();
}
