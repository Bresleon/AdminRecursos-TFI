using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentacion.Models;

public class ProductoVisitaViewModel
{
    public int ProductoId { get; set; }

    public string NumeroSerie { get; set; } = string.Empty;
    public DateTime FechaFinGarantia { get; set; }
    public decimal PrecioUnitario { get; set; }
    public string ConceptoAccion { get; set; } = string.Empty; // "VENTA", "REPARACION", "CONTROL"
    public string? Observacion { get; set; }
}
