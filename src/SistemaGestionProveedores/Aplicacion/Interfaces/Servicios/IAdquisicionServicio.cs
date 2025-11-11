using Dominio.Entidades;
using System.Linq.Expressions;

namespace Aplicacion.Interfaces.Servicios;

public interface IAdquisicionServicio
{
    Task<Adquisicion?> Obtener(Guid id);
    Task<Adquisicion?> Obtener(string numeroSerie);
    Task<IEnumerable<Adquisicion>> ObtenerTodos(Expression<Func<Adquisicion, bool>>? filtro = null);
    Task Agregar(Adquisicion adquisicion);
    Task Modificar(Adquisicion adquisicion);
}
