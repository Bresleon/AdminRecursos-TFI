using Aplicacion.Interfaces.Repositorios;
using Aplicacion.Interfaces.Servicios;
using Dominio.Entidades;
using System.Linq.Expressions;

namespace Aplicacion.Servicios;

public class TipoMantenimientoServicio : ITipoMantenimientoServicio
{
    private readonly ITipoMantenimientoRepositorio _repo;

    public TipoMantenimientoServicio(ITipoMantenimientoRepositorio repo)
    {
        _repo = repo;
    }

    public async Task<TipoMantenimiento?> Obtener(Guid id)
    {
        var equipo = await _repo.ObtenerPorId(id);

        if (equipo == null)
            throw new Exception("No se pudo encontrar el tipo de mantenimiento");

        return equipo;
    }

    public async Task<IEnumerable<TipoMantenimiento>> ObtenerTodos(Expression<Func<TipoMantenimiento, bool>>? filtro = null)
    {
        return await (filtro == null
            ? _repo.ObtenerTodos()
            : _repo.ObtenerTodos(filtro));
    }
}
