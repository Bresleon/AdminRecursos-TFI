using Aplicacion.Interfaces.Servicios;
using Dominio.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentacion.Models.Mantenimientos;

namespace Presentacion.Controllers;

public class MantenimientosController : Controller
{
    private readonly IMantenimientoServicio _mantenimientoServ;
    private readonly ITecnicoServicio _tecnicoServ;
    private readonly ITipoMantenimientoServicio _tipoMantenimientoServ;

    public MantenimientosController(IMantenimientoServicio mantenimientoServ, ITecnicoServicio tecnicoServ, ITipoMantenimientoServicio tipoMantenimientoServ)
    {
        _mantenimientoServ = mantenimientoServ;
        _tecnicoServ = tecnicoServ;
        _tipoMantenimientoServ = tipoMantenimientoServ;
    }

    public async Task<IActionResult> Index()
    {
        var mantenimientos = await _mantenimientoServ.ObtenerTodos();

        var mantenimientosVM = mantenimientos.Select(m => new MantenimientoViewModel
        {
            Id = m.Id,
            NumeroSerie = m.Adquisicion.NumeroSerie,
            Equipo = m.Adquisicion.Equipo.Nombre,
            TipoEquipo = m.Adquisicion.Equipo.TipoEquipo.Nombre,
            Estado = m.Estado,
            Fecha = m.Fecha,
            TipoMantenimiento = m.TipoMantenimiento.Descripcion,
            Descripcion = m.Descripcion,
            Costo = m.Costo,
            Calificacion = m.Calificacion,
            NombreCompletoTecnico = $"{m.Tecnico.Nombre} {m.Tecnico.Apellido}",
            DNITecnico = m.Tecnico.DNI
        }).ToList();

        return View(mantenimientosVM);
    }

    public async Task<IActionResult> Crear()
    {
        var (tecnicos, tiposMantenimiento, estados) = await ObtenerListasDesplegablesMantenimiento();

        var mantenimientoVM = new MantenimientoUpsertViewModel
        {
            TecnicosDisponibles = tecnicos,
            TiposMantenimientoDisponibles = tiposMantenimiento,
            EstadosDisponibles = estados
        };

        return View(mantenimientoVM);
    }

    public async Task<IActionResult> Detalles(Guid id)
    {
        var mantenimiento = await _mantenimientoServ.Obtener(id);

        var mantenimientoVM = new MantenimientoViewModel
        {
            Id = mantenimiento!.Id,
            NumeroSerie = mantenimiento.Adquisicion.NumeroSerie,
            Equipo = mantenimiento.Adquisicion.Equipo.Nombre,
            TipoEquipo = mantenimiento.Adquisicion.Equipo.TipoEquipo.Nombre,
            Estado = mantenimiento.Estado,
            Fecha = mantenimiento.Fecha,
            TipoMantenimiento = mantenimiento.TipoMantenimiento.Descripcion,
            Descripcion = mantenimiento.Descripcion,
            Costo = mantenimiento.Costo,
            Calificacion = mantenimiento.Calificacion,
            NombreCompletoTecnico = $"{mantenimiento.Tecnico.Nombre} {mantenimiento.Tecnico.Apellido}",
            DNITecnico = mantenimiento.Tecnico.DNI
        };

        return View(mantenimientoVM);
    }

    public async Task<IActionResult> Editar(Guid id)
    {
        var mantenimiento = await _mantenimientoServ.Obtener(id);

        var (tecnicos, tiposMantenimiento, estados) = await ObtenerListasDesplegablesMantenimiento();

        var mantenimientoVM = new MantenimientoUpsertViewModel
        {
            Id = id,
            AdquisicionId = mantenimiento!.AdquisicionId,
            TecnicoId = mantenimiento.TecnicoId,
            TipoMantenimientoId = mantenimiento.TipoMantenimientoId,
            NumeroSerie = mantenimiento.Adquisicion.NumeroSerie,
            Equipo = mantenimiento.Adquisicion.Equipo.Nombre,
            TipoEquipo = mantenimiento.Adquisicion.Equipo.TipoEquipo.Nombre,
            Estado = mantenimiento.Estado,
            Fecha = mantenimiento.Fecha,
            Descripcion = mantenimiento.Descripcion,
            Costo = mantenimiento.Costo,
            Calificacion = mantenimiento.Calificacion,
            TecnicosDisponibles = tecnicos,
            TiposMantenimientoDisponibles = tiposMantenimiento,
            EstadosDisponibles = estados
        };

        return View(mantenimientoVM);
    }

    private async Task<(List<SelectListItem>, List<SelectListItem>, List<SelectListItem>)> ObtenerListasDesplegablesMantenimiento()
    {
        // TODO: Debería buscar solo técnicos que trabajen en la misma empresa que el técnico que vendió el equipo
        var tecnicos = await _tecnicoServ.ObtenerTodos();
        var tecnicosSelect = tecnicos.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = $"{t.Nombre} {t.Apellido} - DNI: {t.DNI}"
            }).ToList();

        var tiposMantenimiento = await _tipoMantenimientoServ.ObtenerTodos();
        var tiposMantenimientoSelect = tiposMantenimiento
            .Select(tm => new SelectListItem
            {
                Value = tm.Id.ToString(),
                Text = tm.Descripcion
            }).ToList();

        var estados = new List<SelectListItem>
        {
            new SelectListItem { Value = Estado.PENDIENTE.ToString(), Text = "Pendiente" },
            new SelectListItem { Value = Estado.FINALIZADO.ToString(), Text = "Finalizado" }
        };

        return (tecnicosSelect, tiposMantenimientoSelect, estados);
    }
}
