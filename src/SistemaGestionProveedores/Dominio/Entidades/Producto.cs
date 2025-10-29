namespace Dominio.Entidades;

public class Producto
{
    public Guid Id { get; set; }

    public Guid ProveedorId { get; set; }
    public Proveedor Proveedor { get; set; }

    public Guid TipoProductoId { get; set; }
    public TipoProducto TipoProducto { get; set; }

    public string Nombre { get; set; }
}
