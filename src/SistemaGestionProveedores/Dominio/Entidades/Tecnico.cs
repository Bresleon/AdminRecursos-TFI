namespace Dominio.Entidades;

public class Tecnico : EntidadBase
{
    public Guid ProveedorId { get; set; }
    public Proveedor Proveedor { get; set; }

    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string DNI { get; set; }
    public string Telefono { get; set; }
    public float Calificacion { get; set; }

    public ICollection<Adquisicion> Adquisiciones { get; set; } = new List<Adquisicion>();
    public ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();
}
