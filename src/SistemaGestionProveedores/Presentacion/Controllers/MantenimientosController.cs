using Dominio.Entidades;
using Dominio.Enums;
using Infraestructura.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Presentacion.Models;

namespace Presentacion.Controllers;

public class MantenimientosController : Controller
{
    private readonly ApplicationDbContext _context;

    public MantenimientosController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var mantenimientos = _context.Mantenimientos
            .Include(m => m.Adquisicion)
                .ThenInclude(a => a.Equipo)
                .ThenInclude(e => e.TipoEquipo)
            .Include(m => m.TipoMantenimiento)
            .Include(m => m.Tecnico)
            .ToList();

        var mantenimientosVM = mantenimientos.Select(m => new Models.MantenimientoViewModel
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

    public IActionResult Crear()
    {
        var (tecnicos, tiposMantenimiento, estados) = ObtenerListasDesplegablesMantenimiento();

        var mantenimientoVM = new MantenimientoUpsertViewModel
        {
            TecnicosDisponibles = tecnicos,
            TiposMantenimientoDisponibles = tiposMantenimiento,
            EstadosDisponibles = estados
        };

        return View(mantenimientoVM);
    }

    public IActionResult Detalles(Guid id)
    {
        var mantenimiento = _context.Mantenimientos
            .Include(m => m.Adquisicion)
                .ThenInclude(a => a.Equipo)
                .ThenInclude(e => e.TipoEquipo)
            .Include(m => m.TipoMantenimiento)
            .Include(m => m.Tecnico)
            .FirstOrDefault(m => m.Id == id);

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

    public IActionResult Editar(Guid id)
    {
        var mantenimiento = _context.Mantenimientos
            .Include(m => m.Adquisicion)
                .ThenInclude(a => a.Equipo)
                .ThenInclude(e => e.TipoEquipo)
            .Include(m => m.TipoMantenimiento)
            .Include(m => m.Tecnico)
            .FirstOrDefault(m => m.Id == id);

        var (tecnicos, tiposMantenimiento, estados) = ObtenerListasDesplegablesMantenimiento();

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

    private (List<SelectListItem>, List<SelectListItem>, List<SelectListItem>) ObtenerListasDesplegablesMantenimiento()
    {
        // TODO: Debería buscar solo técnicos que trabajen en la misma empresa que el técnico que vendió el equipo
        var tecnicos = _context.Tecnicos
            .Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = $"{t.Nombre} {t.Apellido} - DNI: {t.DNI}"
            }).ToList();

        var tiposMantenimiento = _context.TiposMantenimiento
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

        return (tecnicos, tiposMantenimiento, estados);
    }
}
