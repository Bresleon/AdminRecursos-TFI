using Dominio.Entidades;
using System.Linq.Expressions;

namespace Aplicacion.Interfaces.Servicios;

public interface IProveedorServicio
{
    Task<Proveedor?> Obtener(Guid id);
    Task<IEnumerable<Proveedor>> ObtenerTodos(Expression<Func<Proveedor, bool>>? filtro = null);
    Task Agregar(Proveedor proveedor);
    Task Modificar(Proveedor proveedor);
    Task Eliminar(Proveedor proveedor);
}
