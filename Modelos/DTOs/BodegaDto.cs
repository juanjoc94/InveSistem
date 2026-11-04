using System.ComponentModel.DataAnnotations;

namespace API.Modelos.DTOs;

public class BodegaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Ubicacion { get; set; } = string.Empty;
    public int Capacidad { get; set; }
}

public class BodegaGuardarDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La ubicación es obligatoria")]
    [StringLength(200)]
    public string Ubicacion { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "La capacidad debe ser mayor a 0")]
    public int Capacidad { get; set; }
}
