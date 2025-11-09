using Dominio.Entidades;
using System.Linq.Expressions;

namespace Aplicacion.Interfaces.Servicios;

public interface ITipoEquipoServicio
{
    Task<TipoEquipo?> Obtener(Guid id);
    Task<IEnumerable<TipoEquipo>> ObtenerTodos(Expression<Func<TipoEquipo, bool>>? filtro = null);
    Task Agregar(TipoEquipo tipoEquipo);
    Task Modificar(TipoEquipo tipoEquipo);
    Task Eliminar(TipoEquipo tipoEquipo);
}
