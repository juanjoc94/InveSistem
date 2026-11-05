using Microsoft.AspNetCore.Mvc;
using Modelos.DTOs;
using API.JWT;
using System.Security.Principal;
namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwt;

    private record UsuarioDemo(string Usuario, string Contrasena, string Rol);

    private static readonly List<UsuarioDemo> _usuarios = new()
    {
        new UsuarioDemo("Juan", "Juan2876", "DirectorTecnico"),
        new UsuarioDemo("Manuel", "M234*", "DirectorOperativo"),
        new UsuarioDemo("Jairo", "JAIRITO23", "JefeDePlanta")
    };

    public AuthController(JwtService jwt)
    {
        _jwt = jwt;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDTO login)
    {
        var encontrado = _usuarios.FirstOrDefault(u => u.Usuario == login.Usuario && u.Contrasena == login.Contrasena);
        if (encontrado == null) return Unauthorized();
        var token = _jwt.GenerarToken(encontrado.Usuario, encontrado.Rol);
        return Ok(new { token });
    }
}
