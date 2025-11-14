using Aplicacion.Interfaces.Repositorios;
using Aplicacion.Interfaces.Servicios;
using Dominio.Entidades;
using System.Linq.Expressions;

namespace Aplicacion.Servicios;

public class TipoEquipoServicio : ITipoEquipoServicio
{
    private readonly ITipoEquipoRepositorio _repo;

    public TipoEquipoServicio(ITipoEquipoRepositorio repo)
    {
        _repo = repo;
    }

    public async Task Agregar(TipoEquipo tipoEquipo)
    {
        var esExitosa = await _repo.Agregar(tipoEquipo);

        if (!esExitosa)
            throw new Exception("No se pudo agregar el tipo de equipo");
    }

    public async Task Eliminar(TipoEquipo tipoEquipo)
    {
        var esExitosa = await _repo.Eliminar(tipoEquipo);

        if (!esExitosa)
            throw new Exception("No se pudo eliminar el tipo de equipo");
    }

    public async Task Modificar(TipoEquipo tipoEquipo)
    {
        var esExitosa = await _repo.Modificar(tipoEquipo);

        if (!esExitosa)
            throw new Exception("No se pudo modificar el tipo de equipo");
    }

    public async Task<TipoEquipo?> Obtener(Guid id)
    {
        var equipo = await _repo.ObtenerPorId(id);

        if (equipo == null)
            throw new Exception("No se pudo encontrar el tipo de equipo");

        return equipo;
    }

    public async Task<IEnumerable<TipoEquipo>> ObtenerTodos(Expression<Func<TipoEquipo, bool>>? filtro = null)
    {
        return await (filtro == null
            ? _repo.ObtenerTodos()
            : _repo.ObtenerTodos(filtro));
    }
}
