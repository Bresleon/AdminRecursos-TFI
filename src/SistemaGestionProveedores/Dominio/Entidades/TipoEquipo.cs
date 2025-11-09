namespace Dominio.Entidades;

public class TipoEquipo : EntidadBase
{
    public string Nombre { get; set; }

    public ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();
}
