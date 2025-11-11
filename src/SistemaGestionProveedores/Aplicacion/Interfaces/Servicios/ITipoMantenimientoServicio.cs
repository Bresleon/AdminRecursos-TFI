using Dominio.Entidades;
using System.Linq.Expressions;

namespace Aplicacion.Interfaces.Servicios;

public interface ITipoMantenimientoServicio
{
    Task<TipoMantenimiento?> Obtener(Guid id);
    Task<IEnumerable<TipoMantenimiento>> ObtenerTodos(Expression<Func<TipoMantenimiento, bool>>? filtro = null);
}
