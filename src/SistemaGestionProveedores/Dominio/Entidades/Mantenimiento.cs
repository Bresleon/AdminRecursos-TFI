using Dominio.Enums;

namespace Dominio.Entidades;

public class Mantenimiento
{
    public Guid Id { get; set; }

    public Guid AdquisicionId { get; set; }
    public Adquisicion Adquisicion { get; set; }

    public Guid TecnicoId { get; set; }
    public Tecnico Tecnico { get; set; }

    public Guid TipoMantenimientoId { get; set; }
    public TipoMantenimiento TipoMantenimiento { get; set; }

    public Estado Estado { get; set; }
    public DateOnly Fecha { get; set; }
    public string Descripcion { get; set; }
    public decimal Costo { get; set; }
    public float Calificacion { get; set; }
}
