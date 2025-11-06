namespace Dominio.Entidades;

public class Proveedor
{
    public Guid Id { get; set; }
    public string RazonSocial { get; set; }
    public string CUIT { get; set; }
    public string? Email { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public float Calificacion { get; set; }

    public ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();
    public ICollection<Tecnico> Tecnicos { get; set; } = new List<Tecnico>();
}
