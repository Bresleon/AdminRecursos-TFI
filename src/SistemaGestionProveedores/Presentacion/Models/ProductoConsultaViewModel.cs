namespace Presentacion.Models;

public class ProductoConsultaViewModel
{
    public string NumeroSerie { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string TipoProducto { get; set; } = string.Empty;

    public DateOnly FechaFinGarantia { get; set; }
    public DateTime FechaAdquisicion { get; set; }

    public string NombreCompletoTecnico { get; set; } = string.Empty;
    public string DNITecnico { get; set; } = string.Empty;
}
