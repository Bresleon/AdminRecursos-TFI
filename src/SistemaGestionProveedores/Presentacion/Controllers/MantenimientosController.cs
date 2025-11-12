using Aplicacion.Interfaces.Servicios;
using Dominio.Entidades;
using Dominio.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentacion.Models.Mantenimientos;

namespace Presentacion.Controllers;

public class MantenimientosController : Controller
{
    private readonly IMantenimientoServicio _mantenimientoServ;
    private readonly IAdquisicionServicio _adquisicionServ;
    private readonly ITecnicoServicio _tecnicoServ;
    private readonly ITipoMantenimientoServicio _tipoMantenimientoServ;

    public MantenimientosController(IMantenimientoServicio mantenimientoServ, 
                                    IAdquisicionServicio adquisicionServ, 
                                    ITecnicoServicio tecnicoServ, 
                                    ITipoMantenimientoServicio tipoMantenimientoServ)
    {
        _mantenimientoServ = mantenimientoServ;
        _adquisicionServ = adquisicionServ;
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
        var (tiposMantenimiento, estados) = await ObtenerListasDesplegablesMantenimiento();

        var mantenimientoVM = new MantenimientoUpsertViewModel
        {
            TiposMantenimientoDisponibles = tiposMantenimiento,
            EstadosDisponibles = estados
        };

        return View(mantenimientoVM);
    }

    [HttpPost]
    public async Task<IActionResult> Crear(MantenimientoUpsertViewModel modelo)
    {
        var (tiposMantenimiento, estados) = await ObtenerListasDesplegablesMantenimiento();
        modelo.TiposMantenimientoDisponibles = tiposMantenimiento;
        modelo.EstadosDisponibles = estados;

        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Uno o varios de los datos son incorrectos o están vacíos");
            return View(modelo);
        }

        var tecnico = await _tecnicoServ.Obtener(modelo.DniTecnico);
        var adquisicion = await _adquisicionServ.Obtener(modelo.NumeroSerie);

        if (tecnico == null)
        {
            ModelState.AddModelError("DniTecnico", "El DNI del técnico no existe");
            return View(modelo);
        }

        if (adquisicion == null)
        {
            ModelState.AddModelError("NumeroSerie", "El número de serie del equipo no existe");
            return View(modelo);
        }

        if (tecnico.ProveedorId != adquisicion.Tecnico.ProveedorId)
        {
            ModelState.AddModelError("", "El técnico que realice el mantenimiento debe pertenecer a la misma empresa de quien vendió el equipo");
            return View(modelo);
        }

        var mantenimiento = new Mantenimiento
        {
            AdquisicionId = modelo.AdquisicionId,
            TecnicoId = modelo.TecnicoId,
            TipoMantenimientoId = modelo.TipoMantenimientoId,
            Estado = modelo.Estado,
            Fecha = modelo.Fecha,
            Descripcion = modelo.Descripcion,
            Costo = modelo.Costo,
            Calificacion = modelo.Calificacion,
        };

        try
        {
            await _mantenimientoServ.Agregar(mantenimiento);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", e.Message);
            return View(modelo);
        }

        return RedirectToAction(nameof(Index));
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

        var (tiposMantenimiento, estados) = await ObtenerListasDesplegablesMantenimiento();

        var mantenimientoVM = new MantenimientoUpsertViewModel
        {
            Id = id,
            AdquisicionId = mantenimiento!.AdquisicionId,
            TecnicoId = mantenimiento.TecnicoId,
            TipoMantenimientoId = mantenimiento.TipoMantenimientoId,
            DniTecnico = mantenimiento.Tecnico.DNI,
            NombreCompletoTecnico = mantenimiento.Tecnico.Nombre + " " + mantenimiento.Tecnico.Apellido,
            NumeroSerie = mantenimiento.Adquisicion.NumeroSerie,
            Equipo = mantenimiento.Adquisicion.Equipo.Nombre,
            TipoEquipo = mantenimiento.Adquisicion.Equipo.TipoEquipo.Nombre,
            Estado = mantenimiento.Estado,
            Fecha = mantenimiento.Fecha,
            Descripcion = mantenimiento.Descripcion,
            Costo = mantenimiento.Costo,
            Calificacion = mantenimiento.Calificacion,
            TiposMantenimientoDisponibles = tiposMantenimiento,
            EstadosDisponibles = estados
        };

        return View(mantenimientoVM);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(MantenimientoUpsertViewModel modelo)
    {
        var (tiposMantenimiento, estados) = await ObtenerListasDesplegablesMantenimiento();
        modelo.TiposMantenimientoDisponibles = tiposMantenimiento;
        modelo.EstadosDisponibles = estados;

        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Uno o varios de los datos son incorrectos o están vacíos");
            return View(modelo);
        }

        var tecnico = await _tecnicoServ.Obtener(modelo.DniTecnico);
        var adquisicion = await _adquisicionServ.Obtener(modelo.NumeroSerie);

        if (tecnico == null)
        {
            ModelState.AddModelError("DniTecnico", "El DNI del técnico no existe");
            return View(modelo);
        }

        if (adquisicion == null)
        {
            ModelState.AddModelError("NumeroSerie", "El número de serie del equipo no existe");
            return View(modelo);
        }

        if (tecnico.ProveedorId != adquisicion.Tecnico.ProveedorId)
        {
            ModelState.AddModelError("", "El técnico que realice el mantenimiento debe pertenecer a la misma empresa de quien vendió el equipo");
            return View(modelo);
        }

        var mantenimiento = new Mantenimiento
        {
            Id = modelo.Id,
            AdquisicionId = modelo.AdquisicionId,
            TecnicoId = modelo.TecnicoId,
            TipoMantenimientoId = modelo.TipoMantenimientoId,
            Estado = modelo.Estado,
            Fecha = modelo.Fecha,
            Descripcion = modelo.Descripcion,
            Costo = modelo.Costo,
            Calificacion = modelo.Calificacion,
        };

        try
        {
            await _mantenimientoServ.Modificar(mantenimiento);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", e.Message);
            return View(modelo);
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<(List<SelectListItem>, List<SelectListItem>)> ObtenerListasDesplegablesMantenimiento()
    {
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

        return (tiposMantenimientoSelect, estados);
    }
}
