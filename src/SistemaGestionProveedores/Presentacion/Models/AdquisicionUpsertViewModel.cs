using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentacion.Models;

public class AdquisicionUpsertViewModel
{
    public Guid Id { get; set; }
    public Guid TecnicoId { get; set; }
    public Guid EquipoId { get; set; }

    public string NumeroSerie { get; set; } = string.Empty;
    public DateOnly FechaAdquisicion { get; set; }
    public DateOnly FechaFinGarantia { get; set; }
    public decimal Costo { get; set; }

    public string? NombreCompletoTecnico { get; set; }
    public string? NombreProveedor { get; set; }

    public List<SelectListItem> EquiposDisponibles { get; set; } = new();
}

