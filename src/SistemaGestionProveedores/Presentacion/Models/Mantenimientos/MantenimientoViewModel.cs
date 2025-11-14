using Dominio.Enums;

namespace Presentacion.Models.Mantenimientos;

public class MantenimientoViewModel
{
    public Guid Id { get; set; }
    public string NumeroSerie { get; set; } = string.Empty;
    public string Equipo { get; set; } = string.Empty;
    public string TipoEquipo { get; set; } = string.Empty;

    public Estado Estado { get; set; }
    public DateOnly Fecha { get; set; }
    public string TipoMantenimiento { get; set; } = string.Empty;
    public string Descripcion { get; set; }
    public decimal Costo { get; set; }
    public float Calificacion { get; set; }

    public string NombreCompletoTecnico { get; set; } = string.Empty;
    public string DNITecnico { get; set; } = string.Empty;
}
