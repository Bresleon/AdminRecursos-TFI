using Aplicacion.Interfaces.Repositorios;
using Aplicacion.Interfaces.Servicios;
using Dominio.Entidades;
using System.Linq.Expressions;

namespace Aplicacion.Servicios;

public class MantenimientoServicio : IMantenimientoServicio
{
    private readonly IMantenimientoRepositorio _repo;
    private readonly ITecnicoServicio _tecnicoServ;
    private readonly IProveedorServicio _proveedorServ;

    public MantenimientoServicio(IMantenimientoRepositorio repo, ITecnicoServicio tecnicoServ, IProveedorServicio proveedorServ)
    {
        _repo = repo;
        _tecnicoServ = tecnicoServ;
        _proveedorServ = proveedorServ;
    }

    public async Task Agregar(Mantenimiento mantenimiento)
    {
        var esExitosa = await _repo.Agregar(mantenimiento);

        if (mantenimiento.Calificacion != 0)
        {
            // Calificacion técnico

            var tecnico = await _tecnicoServ.Obtener(mantenimiento.TecnicoId);

            var sumaCalificacionesTecnicos = tecnico!.Mantenimientos.Sum(m => m.Calificacion);
            var cantMantenimientos = tecnico!.Mantenimientos.Count;

            var nuevaCalificacionTecnico = sumaCalificacionesTecnicos / cantMantenimientos;

            tecnico.Calificacion = nuevaCalificacionTecnico;

            await _tecnicoServ.Modificar(tecnico);

            // Calificación proveedor

            var proveedor = await _proveedorServ.Obtener(tecnico.ProveedorId);

            var sumaCalificacionesProveedores = proveedor!.Tecnicos.Sum(t => t.Calificacion);
            var cantTecnicos = proveedor!.Tecnicos.Count;

            var nuevaCalificacionProveedor = sumaCalificacionesProveedores / cantTecnicos;

            proveedor.Calificacion = nuevaCalificacionProveedor;

            await _proveedorServ.Modificar(proveedor);
        }

        if (!esExitosa)
            throw new Exception("No se pudo agregar el mantenimiento");
    }

    public async Task Modificar(Mantenimiento mantenimiento)
    {
        var esExitosa = await _repo.Modificar(mantenimiento);

        if (mantenimiento.Calificacion != 0)
        {
            // Calificacion técnico

            var tecnico = await _tecnicoServ.Obtener(mantenimiento.TecnicoId);

            var sumaCalificacionesTecnicos = tecnico!.Mantenimientos.Sum(m => m.Calificacion) + mantenimiento.Calificacion;
            var cantMantenimientos = tecnico!.Mantenimientos.Count;

            var nuevaCalificacionTecnico = sumaCalificacionesTecnicos / cantMantenimientos;

            tecnico.Calificacion = nuevaCalificacionTecnico;

            await _tecnicoServ.Modificar(tecnico);

            // Calificación proveedor

            var proveedor = await _proveedorServ.Obtener(tecnico.ProveedorId);

            var sumaCalificacionesProveedores = proveedor!.Tecnicos.Sum(t => t.Calificacion);
            var cantTecnicos = proveedor!.Tecnicos.Count;

            var nuevaCalificacionProveedor = sumaCalificacionesProveedores / cantTecnicos;

            proveedor.Calificacion = nuevaCalificacionProveedor;

            await _proveedorServ.Modificar(proveedor);
        }

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
