using Dominio.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentacion.Models.Mantenimientos;

public class MantenimientoUpsertViewModel
{
    public Guid Id { get; set; }
    public Guid AdquisicionId { get; set; }
    public Guid TecnicoId { get; set; }
    public Guid TipoMantenimientoId { get; set; }

    // Solo lectura o informativos
    public string NumeroSerie { get; set; } = string.Empty;
    public string DniTecnico { get; set; } = string.Empty; 
    public string Equipo { get; set; } = string.Empty;
    public string TipoEquipo { get; set; } = string.Empty;

    public Estado Estado { get; set; }
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public string Descripcion { get; set; } = string.Empty;
    public decimal Costo { get; set; }
    public float Calificacion { get; set; }

    // Para desplegables
    public List<SelectListItem> TiposMantenimientoDisponibles { get; set; } = new();
    public List<SelectListItem> EstadosDisponibles { get; set; } = new();
}
