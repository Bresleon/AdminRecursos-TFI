using Aplicacion.Interfaces.Repositorios;
using Aplicacion.Interfaces.Servicios;
using Dominio.Entidades;
using System.Linq.Expressions;

namespace Aplicacion.Servicios;

public class MantenimientoServicio : IMantenimientoServicio
{
    private readonly IMantenimientoRepositorio _repo;

    public MantenimientoServicio(IMantenimientoRepositorio repo)
    {
        _repo = repo;
    }

    public async Task Agregar(Mantenimiento mantenimiento)
    {
        var esExitosa = await _repo.Agregar(mantenimiento);

        if (!esExitosa)
            throw new Exception("No se pudo agregar el mantenimiento");
    }

    public async Task Modificar(Mantenimiento mantenimiento)
    {
        var esExitosa = await _repo.Modificar(mantenimiento);

        if (!esExitosa)
            throw new Exception("No se pudo modificar el mantenimiento");
    }

    public async Task<Mantenimiento?> Obtener(Guid id)
    {
        var mantenimiento = await _repo.ObtenerPorId(id, m => m.Adquisicion.Equipo.TipoEquipo, m => m.Tecnico.Proveedor.Equipos, m => m.TipoMantenimiento);

        if (mantenimiento == null)
            throw new Exception("No se pudo encontrar el mantenimiento");

        return mantenimiento;
    }

    public async Task<Mantenimiento?> Obtener(string numeroSerie)
    {
        var mantenimiento = await _repo.Obtener(m => m.Adquisicion.NumeroSerie == numeroSerie, m => m.Adquisicion.Equipo.TipoEquipo, m => m.Tecnico.Proveedor.Equipos, m => m.TipoMantenimiento);

        if (mantenimiento == null)
            throw new Exception("No se pudo encontrar el mantenimiento");

        return mantenimiento;
    }

    public async Task<IEnumerable<Mantenimiento>> ObtenerTodos(Expression<Func<Mantenimiento, bool>>? filtro = null)
    {
        return await (filtro == null
            ? _repo.ObtenerTodos(includes: [m => m.Adquisicion.Equipo.TipoEquipo, m => m.Tecnico.Proveedor.Equipos, m => m.TipoMantenimiento])
            : _repo.ObtenerTodos(filtro, includes: [m => m.Adquisicion.Equipo.TipoEquipo, m => m.Tecnico.Proveedor.Equipos, m => m.TipoMantenimiento]));
    }
}
