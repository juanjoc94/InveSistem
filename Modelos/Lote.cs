namespace API.Modelos;

public class Lote
{
    public int Id { get; set; }
    public int ProductoId { get; set; }
    public int BodegaId { get; set; }
    public int Cantidad { get; set; }
    public DateTime? FechaVencimiento { get; set; }
}
