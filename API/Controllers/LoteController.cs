using Modelos.DTOs;
using Servicios;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoteController : ControllerBase
{
    private readonly IServicioLote _servicio;

    public LoteController(IServicioLote servicio) => _servicio = servicio;

    [HttpGet]
    public ActionResult<IEnumerable<LoteDto>> Get() => Ok(_servicio.ObtenerTodos());

    [HttpGet("{id:int}")]
    public ActionResult<LoteDto> Get(int id)
    {
        var item = _servicio.ObtenerPorId(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public ActionResult<LoteDto> Post(LoteGuardarDto dto)
    {
        try
        {
            var creado = _servicio.Crear(dto);
            return CreatedAtAction(nameof(Get), new { id = creado.Id }, creado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public ActionResult<LoteDto> Put(int id, LoteGuardarDto dto)
    {
        try
        {
            var actualizado = _servicio.Actualizar(id, dto);
            return actualizado is null ? NotFound() : Ok(actualizado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id) =>
        _servicio.Eliminar(id) ? NoContent() : NotFound();
}
