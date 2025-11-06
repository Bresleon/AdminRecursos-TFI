namespace Dominio.Entidades;

public class TipoEquipo
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }

    public ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();
}
