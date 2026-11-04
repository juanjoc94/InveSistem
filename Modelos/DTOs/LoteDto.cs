using System.ComponentModel.DataAnnotations;

namespace Modelos.DTOs;

public class LoteDto
{
    public int Id { get; set; }
    public int ProductoId { get; set; }
    public int BodegaId { get; set; }
    public int Cantidad { get; set; }
    public DateTime? FechaVencimiento { get; set; }
}

public class LoteGuardarDto
{
    [Range(1, int.MaxValue, ErrorMessage = "ProductoId es obligatorio")]
    public int ProductoId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "BodegaId es obligatorio")]
    public int BodegaId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
    public int Cantidad { get; set; }

    public DateTime? FechaVencimiento { get; set; }
}
