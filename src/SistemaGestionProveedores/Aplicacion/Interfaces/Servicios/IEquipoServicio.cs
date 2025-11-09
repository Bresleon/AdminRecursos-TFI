using Dominio.Entidades;
using System.Linq.Expressions;

namespace Aplicacion.Interfaces.Servicios;

public interface IEquipoServicio
{
    Task<Equipo?> Obtener(Guid id);
    Task<IEnumerable<Equipo>> ObtenerTodos(Expression<Func<Equipo, bool>>? filtro = null);
    Task Agregar(Equipo equipo);
    Task Modificar(Equipo equipo);
    Task Eliminar(Equipo equipo);
}
