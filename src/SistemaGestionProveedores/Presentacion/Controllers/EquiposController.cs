using Aplicacion.Interfaces.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentacion.Models.Equipos;

namespace Presentacion.Controllers;

public class EquiposController : Controller
{
    private readonly IEquipoServicio _equipoServ;
    private readonly IProveedorServicio _proveedorServ;
    private readonly ITipoEquipoServicio _tipoEquipoServ;

    public EquiposController(IEquipoServicio equipoServ, IProveedorServicio proveedorServ, ITipoEquipoServicio tipoEquipoServ)
    {
        _equipoServ = equipoServ;
        _proveedorServ = proveedorServ;
        _tipoEquipoServ = tipoEquipoServ;
    }

    public async Task<IActionResult> Index()
    {
        var equipos = await _equipoServ.ObtenerTodos();

        var equiposVM = equipos.Select(e => new EquipoViewModel
        {
            Id = e.Id,
            Nombre = e.Nombre,
            ProveedorId = e.ProveedorId,
            ProveedorNombre = e.Proveedor?.RazonSocial,
            TipoEquipoId = e.TipoEquipoId,
            TipoEquipoNombre = e.TipoEquipo?.Nombre
        }).ToList();

        return View(equiposVM);
    }

    public async Task<IActionResult> Crear()
    {
        var proveedores = await _proveedorServ.ObtenerTodos();
        var tiposEquipo = await _tipoEquipoServ.ObtenerTodos();

        var equipoVM = new EquipoViewModel
        {
            ProveedoresDisponibles = proveedores.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.RazonSocial
            }).ToList(),

            TiposEquipoDisponibles = tiposEquipo.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Nombre
            }).ToList()
        };

        return View(equipoVM);
    }

    public async Task<IActionResult> Editar(Guid id)
    {
        var equipo = await _equipoServ.Obtener(id);

        if (equipo == null)
        {
            return NotFound();
        }

        var proveedores = await _proveedorServ.ObtenerTodos();
        var tiposEquipo = await _tipoEquipoServ.ObtenerTodos();

        var equipoVM = new EquipoViewModel
        {
            Id = equipo.Id,
            Nombre = equipo.Nombre,
            ProveedorId = equipo.ProveedorId,
            TipoEquipoId = equipo.TipoEquipoId,

            ProveedoresDisponibles = proveedores.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.RazonSocial
            }).ToList(),

            TiposEquipoDisponibles = tiposEquipo.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Nombre
            }).ToList()
        };

        return View(equipoVM);
    }

    public async Task<IActionResult> Eliminar(Guid id)
    {
        var equipo = await _equipoServ.Obtener(id);

        if (equipo == null)
        {
            return NotFound();
        }

        var equipoVM = new EquipoViewModel
        {
            Id = equipo.Id,
            Nombre = equipo.Nombre,
            ProveedorNombre = equipo.Proveedor?.RazonSocial,
            TipoEquipoNombre = equipo.TipoEquipo?.Nombre
        };

        return View(equipoVM);
    }
}
