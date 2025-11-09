using Dominio.Entidades;
using System.Linq.Expressions;

namespace Aplicacion.Interfaces.Servicios;

public interface ITecnicoServicio
{
    Task<Tecnico?> Obtener(Guid id);
    Task<Tecnico?> Obtener(string dni);
    Task<IEnumerable<Tecnico>> ObtenerTodos(Expression<Func<Tecnico, bool>>? filtro = null);
    Task Agregar(Tecnico tecnico);
    Task Modificar(Tecnico tecnico);
    Task Eliminar(Tecnico tecnico);
}
