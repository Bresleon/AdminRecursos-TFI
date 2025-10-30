namespace Presentacion.Models;

public class ProveedorViewModel
{
    public Guid Id { get; set; }
    public string RazonSocial { get; set; } = string.Empty;
    public string CUIT { get; set; } = string.Empty;
    public decimal MontoTotalPagado { get; set; } = 0;
    public double Calificacion { get; set; } = 0;
}
