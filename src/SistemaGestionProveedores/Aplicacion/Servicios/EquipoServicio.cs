using Aplicacion.Interfaces.Repositorios;
using Aplicacion.Interfaces.Servicios;
using Dominio.Entidades;
using System.Linq.Expressions;

namespace Aplicacion.Servicios;

public class EquipoServicio : IEquipoServicio
{
    private readonly IEquipoRepositorio _repo;

    public EquipoServicio(IEquipoRepositorio repo)
    {
        _repo = repo;
    }

    public async Task Agregar(Equipo equipo)
    {
        var esExitosa = await _repo.Agregar(equipo);

        if (!esExitosa)
            throw new Exception("No se pudo agregar el equipo");
    }

    public async Task Eliminar(Equipo equipo)
    {
        var esExitosa = await _repo.Eliminar(equipo);

        if (!esExitosa)
            throw new Exception("No se pudo eliminar el equipo");
    }

    public async Task Modificar(Equipo equipo)
    {
        var esExitosa = await _repo.Modificar(equipo);

        if (!esExitosa)
            throw new Exception("No se pudo modificar el equipo");
    }

    public async Task<Equipo?> Obtener(Guid id)
    {
        var equipo = await _repo.ObtenerPorId(id, e => e.Proveedor, e => e.TipoEquipo);

        if (equipo == null)
            throw new Exception("No se pudo encontrar el equipo");

        return equipo;
    }

    public async Task<IEnumerable<Equipo>> ObtenerTodos(Expression<Func<Equipo, bool>>? filtro = null)
    {
        return await (filtro == null
            ? _repo.ObtenerTodos(includes: [e => e.Proveedor, e => e.TipoEquipo])
            : _repo.ObtenerTodos(filtro, includes: [e => e.Proveedor, e => e.TipoEquipo]));
    }
}
