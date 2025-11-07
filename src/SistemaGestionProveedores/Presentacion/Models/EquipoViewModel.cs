using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentacion.Models;

public class EquipoViewModel
{
    public Guid Id { get; set; }

    public Guid ProveedorId { get; set; }
    public string? ProveedorNombre { get; set; }

    public Guid TipoEquipoId { get; set; }
    public string? TipoEquipoNombre { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public List<SelectListItem> ProveedoresDisponibles { get; set; } = new();
    public List<SelectListItem> TiposEquipoDisponibles { get; set; } = new();
}

