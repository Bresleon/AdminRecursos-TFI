using Dominio.Entidades;
using System.Linq.Expressions;

namespace Aplicacion.Interfaces.Repositorios;

public interface IRepositorio<T> where T : EntidadBase
{
    Task<T?> ObtenerPorId(Guid id, params Expression<Func<T, object>>[] includes);
    Task<T?> Obtener(Expression<Func<T, bool>> filtro, params Expression<Func<T, object>>[] includes);
    Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null, params Expression<Func<T, object>>[] includes);
    Task<bool> Agregar(T entidad);
    Task<bool> Eliminar(T entidad);
    Task<bool> Modificar(T entidad);
}