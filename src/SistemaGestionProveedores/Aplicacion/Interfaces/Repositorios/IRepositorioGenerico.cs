using System.Linq.Expressions;

namespace Aplicacion.Interfaces.Repositorios;

public interface IRepositorio<T> where T : class
{
    Task<T?> ObtenerPorId(Guid id);
    Task<T?> Obtener(Expression<Func<T, bool>> filtro);
    Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null);
    Task<bool> Agregar(T obj);
    Task<bool> Eliminar(T obj);
    Task<bool> Modificar(T obj);
}
