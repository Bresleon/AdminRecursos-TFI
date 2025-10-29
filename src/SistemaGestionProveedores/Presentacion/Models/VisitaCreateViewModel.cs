using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentacion.Models;

public class VisitaCreateViewModel
{
    public int TecnicoId { get; set; }
    public List<SelectListItem> Tecnicos { get; set; } = new();
    public List<SelectListItem> ProductosOfrecidos { get; set; } = new();
    public List<ProductoVisitaViewModel> ProductosVisita { get; set; } = new();
    public decimal MontoTotal { get; set; }
    public string? Observaciones { get; set; }
}