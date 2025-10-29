namespace Dominio.Entidades;

public class Proveedor
{
    public Guid Id { get; set; }
    public string RazonSocial { get; set; } = string.Empty;
    public string CUIT { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public double Calificacion { get; set; } = 0;

    public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    public ICollection<Tecnico> Tecnicos { get; set; } = new List<Tecnico>();
}
