namespace Presentacion.Models.Proveedores;

public class ProveedorViewModel
{
    public Guid Id { get; set; }
    public string RazonSocial { get; set; } = string.Empty;
    public string CUIT { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public decimal MontoTotalPagado { get; set; } = 0;
    public float Calificacion { get; set; } = 0;
}
