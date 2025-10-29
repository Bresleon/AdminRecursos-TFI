namespace Dominio.Entidades;

public class Tecnico
{
    public Guid Id { get; set; }

    public Guid ProveedorId { get; set; }
    public Proveedor Proveedor { get; set; }

    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string DNI { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public double Calificacion { get; set; } = 0;

    public ICollection<Visita> Visitas { get; set; } = new List<Visita>();
}
