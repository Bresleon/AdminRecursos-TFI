using Aplicacion.Interfaces.Repositorios;
using Aplicacion.Interfaces.Servicios;
using Dominio.Entidades;
using System.Linq.Expressions;

namespace Aplicacion.Servicios;

public class AdquisicionServicio : IAdquisicionServicio
{
    private readonly IAdquisicionRepositorio _repo;

    public AdquisicionServicio(IAdquisicionRepositorio repo)
    {
        _repo = repo;
    }

    public async Task Agregar(Adquisicion adquisicion)
    {
        var esExitosa = await _repo.Agregar(adquisicion);

        if (!esExitosa)
            throw new Exception("No se pudo agregar la adquisicion");
    }

    public async Task Modificar(Adquisicion adquisicion)
    {
        var esExitosa = await _repo.Modificar(adquisicion);

        if (!esExitosa)
            throw new Exception("No se pudo modificar la adquisicion");
    }

    public async Task<Adquisicion?> Obtener(Guid id)
    {
        var adquisicion = await _repo.ObtenerPorId(id, a => a.Equipo.TipoEquipo, a => a.Tecnico.Proveedor.Equipos);

        if (adquisicion == null)
            throw new Exception("No se pudo encontrar la adquisicion");

        return adquisicion;
    }

    public async Task<Adquisicion?> Obtener(string numeroSerie)
    {
        var adquisicion = await _repo.Obtener(a => a.NumeroSerie == numeroSerie, a => a.Equipo.TipoEquipo, a => a.Tecnico.Proveedor.Equipos);

        if (adquisicion == null)
            throw new Exception("No se pudo encontrar la adquisicion");

        return adquisicion;
    }

    public async Task<IEnumerable<Adquisicion>> ObtenerTodos(Expression<Func<Adquisicion, bool>>? filtro = null)
    {
        return await (filtro == null
            ? _repo.ObtenerTodos(includes: [a => a.Equipo.TipoEquipo, a => a.Tecnico.Proveedor.Equipos])
            : _repo.ObtenerTodos(filtro, includes: [a => a.Equipo.TipoEquipo, a => a.Tecnico.Proveedor.Equipos]));
    }
}
