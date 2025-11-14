namespace Dominio.Entidades;

public class TipoMantenimiento : EntidadBase
{
    public string Descripcion { get; set; }

    public ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();
}
