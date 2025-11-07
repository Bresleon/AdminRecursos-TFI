namespace Presentacion.Models;

public class AdquisicionRecienteViewModel
{
    public string NumeroSerie { get; set; }
    public string Equipo { get; set; }
    public string Tecnico { get; set; }
    public string Proveedor { get; set; }
    public DateOnly FechaAdquisicion { get; set; }
    public decimal Costo { get; set; }
}
