namespace Dominio.Entidades;

public class Proveedor
{
    public Guid Id { get; set; }
    public string RazonSocial { get; set; }
    public string CUIT { get; set; }
    public string Email { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public double Calificacion { get; set; } = 0;
}
