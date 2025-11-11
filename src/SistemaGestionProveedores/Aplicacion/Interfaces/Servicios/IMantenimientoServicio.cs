using Dominio.Entidades;
using System.Linq.Expressions;

namespace Aplicacion.Interfaces.Servicios;

public interface IMantenimientoServicio
{
    Task<Mantenimiento?> Obtener(Guid id);
    Task<Mantenimiento?> Obtener(string numeroSerie);
    Task<IEnumerable<Mantenimiento>> ObtenerTodos(Expression<Func<Mantenimiento, bool>>? filtro = null);
    Task Agregar(Mantenimiento mantenimiento);
    Task Modificar(Mantenimiento mantenimiento);
}
