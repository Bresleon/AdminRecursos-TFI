using Dominio.Entidades;
using Infraestructura.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Presentacion.Models;
using System.Threading.Tasks;

namespace Presentacion.Controllers;

public class EquiposController : Controller
{
    private readonly ApplicationDbContext _context;

    public EquiposController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var equipos = _context.Equipos
            .Include(e => e.Proveedor)
            .Include(e => e.TipoEquipo)
            .ToList();

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

    public IActionResult Crear()
    {
        var proveedores = _context.Proveedores.ToList();
        var tiposEquipo = _context.TiposEquipo.ToList();

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

    public IActionResult Editar(Guid id)
    {
        var equipo = _context.Equipos.Find(id);

        if (equipo == null)
        {
            return NotFound();
        }

        var proveedores = _context.Proveedores.ToList();
        var tiposEquipo = _context.TiposEquipo.ToList();

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
        var equipo = await _context.Equipos
            .Include(e => e.Proveedor)
            .Include(e => e.TipoEquipo)
            .FirstOrDefaultAsync(e => e.Id == id);

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
