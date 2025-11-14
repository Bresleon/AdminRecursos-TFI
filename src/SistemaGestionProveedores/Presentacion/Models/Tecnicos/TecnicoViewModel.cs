using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentacion.Models.Tecnicos;

public class TecnicoViewModel
{
    public Guid Id { get; set; }

    public Guid ProveedorId { get; set; }
    public string? ProveedorNombre { get; set; } = string.Empty;

    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string DNI { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public float Calificacion { get; set; } = 0;

    public List<SelectListItem> ProveedoresDisponibles { get; set; } = new();
}
