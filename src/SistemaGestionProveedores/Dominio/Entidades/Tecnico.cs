namespace Dominio.Entidades;

public class Tecnico
{
    public Guid Id { get; set; }

    public Guid ProveedorId { get; set; }
    public Proveedor Proveedor { get; set; }

    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string DNI { get; set; }
    public string Telefono { get; set; }
    public double Calificacion { get; set; } = 0;
}
