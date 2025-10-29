using Dominio.Enums;

namespace Dominio.Entidades;

public class ProductoEnVisita
{
    public Guid Id { get; set; }
    
    public Guid ProductoId { get; set; }
    public Producto Producto { get; set; }

    public Guid VisitaId { get; set; }
    public Visita Visita { get; set; }

    public string NumeroSerie { get; set; } = string.Empty;
    public decimal PrecioUnitario { get; set; }
    public DateOnly FinGarantia { get; set; }
    public string? Observaciones { get; set; }

    public ConceptoAccion Concepto { get; set; }
}
