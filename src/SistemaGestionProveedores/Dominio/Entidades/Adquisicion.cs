namespace Dominio.Entidades;

public class Adquisicion : EntidadBase
{
    public Guid EquipoId { get; set; }
    public Equipo Equipo { get; set; }

    public Guid TecnicoId { get; set; }
    public Tecnico Tecnico { get; set; }

    public string NumeroSerie { get; set; }
    public DateOnly FechaAdquisicion { get; set; }
    public DateOnly FechaFinGarantia { get; set; }
    public decimal Costo { get; set; }

    public ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();
}
