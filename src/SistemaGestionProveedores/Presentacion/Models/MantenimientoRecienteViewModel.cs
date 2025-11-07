namespace Presentacion.Models;

public class MantenimientoRecienteViewModel
{
    public string NumeroSerie { get; set; }
    public string Tecnico { get; set; }
    public string TipoMantenimiento { get; set; }
    public DateOnly Fecha { get; set; }
    public decimal Costo { get; set; }
    public string Estado { get; set; }
}
