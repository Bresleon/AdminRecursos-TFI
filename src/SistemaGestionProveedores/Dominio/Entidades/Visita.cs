using Dominio.Enums;

namespace Dominio.Entidades;

public class Visita
{
    public Guid Id { get; set; }

    public Guid TecnicoId { get; set; }
    public Tecnico Tecnico { get; set; }

    public decimal MontoTotal { get; set; }
    public DateTime FechaHora { get; set; }
    public string? Observaciones { get; set; }
    
    public double Calificacion { get; set; } = 0;
    public EstadoVisita Estado { get; set; } = EstadoVisita.PENDIENTE;
}
