namespace Presentacion.Models.Adquisiciones;

public class AdquisicionViewModel
{
    public Guid Id { get; set; }
    public string NumeroSerie { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string TipoEquipo { get; set; } = string.Empty;
    public decimal Costo { get; set; }

    public DateOnly FechaFinGarantia { get; set; }
    public DateOnly FechaAdquisicion { get; set; }

    public string NombreCompletoTecnico { get; set; } = string.Empty;
    public string DNITecnico { get; set; } = string.Empty;
}
