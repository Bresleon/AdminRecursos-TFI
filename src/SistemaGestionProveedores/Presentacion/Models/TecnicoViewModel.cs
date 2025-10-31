namespace Presentacion.Models;

public class TecnicoViewModel
{
    public Guid Id { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public string DNI { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Proveedor { get; set; } = string.Empty;
    public double Calificacion { get; set; } = 0;
}
