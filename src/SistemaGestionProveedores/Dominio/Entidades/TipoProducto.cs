namespace Dominio.Entidades;

public class TipoProducto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    public ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
