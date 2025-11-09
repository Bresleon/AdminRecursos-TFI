using Aplicacion.Interfaces.Repositorios;
using Aplicacion.Interfaces.Servicios;
using Dominio.Entidades;
using System.Linq.Expressions;

namespace Aplicacion.Servicios;

public class TecnicoServicio : ITecnicoServicio
{
    private readonly ITecnicoRepositorio _repo;

    public TecnicoServicio(ITecnicoRepositorio repo)
    {
        _repo = repo;
    }

    public async Task Agregar(Tecnico tecnico)
    {
        var esExitosa = await _repo.Agregar(tecnico);

        if (!esExitosa)
            throw new Exception("No se pudo agregar el tecnico");
    }

    public async Task Eliminar(Tecnico tecnico)
    {
        var esExitosa = await _repo.Eliminar(tecnico);

        if (!esExitosa)
            throw new Exception("No se pudo eliminar el tecnico");
    }

    public async Task Modificar(Tecnico tecnico)
    {
        var esExitosa = await _repo.Modificar(tecnico);

        if (!esExitosa)
            throw new Exception("No se pudo modificar el tecnico");
    }

    public async Task<Tecnico?> Obtener(Guid id)
    {
        var tecnico = await _repo.ObtenerPorId(id, t => t.Proveedor);

        if (tecnico == null)
            throw new Exception("No se pudo encontrar el tecnico");

        return tecnico;
    }

    public async Task<Tecnico?> Obtener(string dni)
    {
        var tecnico = await _repo.Obtener(t => t.DNI == dni, t => t.Proveedor);

        if (tecnico == null)
            throw new Exception("No se pudo encontrar el tecnico");

        return tecnico;
    }

    public async Task<IEnumerable<Tecnico>> ObtenerTodos(Expression<Func<Tecnico, bool>>? filtro = null)
    {
        return await (filtro == null ? _repo.ObtenerTodos() : _repo.ObtenerTodos(filtro));
    }
}
