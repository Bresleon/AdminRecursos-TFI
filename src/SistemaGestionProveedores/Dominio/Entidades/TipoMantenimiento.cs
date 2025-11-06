namespace Dominio.Entidades;

public class TipoMantenimiento
{
    public Guid Id { get; set; }
    public string Descripcion { get; set; }

    public ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();
}
