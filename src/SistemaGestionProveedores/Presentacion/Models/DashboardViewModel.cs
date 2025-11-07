namespace Presentacion.Models;

public class DashboardViewModel
{
    public List<ProveedorRankingViewModel> MejoresProveedores { get; set; } = new();
    public List<ProveedorIngresosViewModel> ProveedoresConMasIngresos { get; set; } = new();

    public int TotalProveedores { get; set; }
    public int TotalAdquisiciones { get; set; }
    public int TotalMantenimientos { get; set; }
    public decimal MontoTotalInvertido { get; set; }

    public List<AdquisicionRecienteViewModel> UltimasAdquisiciones { get; set; } = new();
    public List<MantenimientoRecienteViewModel> UltimosMantenimientos { get; set; } = new();
}