namespace Dominio.Entidades;

public class Equipo : EntidadBase
{
    public Guid ProveedorId { get; set; }
    public Proveedor Proveedor { get; set; }

    public Guid TipoEquipoId { get; set; }
    public TipoEquipo TipoEquipo { get; set; }

    public string Nombre { get; set; }

    public int Activado { get; set; } = 1;

    public ICollection<Adquisicion> Adquisiciones { get; set; } = new List<Adquisicion>();
}
