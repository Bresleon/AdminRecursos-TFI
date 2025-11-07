using Infraestructura.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Presentacion.Models.Tecnicos;

namespace Presentacion.Controllers;

public class TecnicosController : Controller
{
    private readonly ApplicationDbContext _context;

    public TecnicosController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(string? dni = null)
    {
        var tecnicos = _context.Tecnicos.AsQueryable();

        if (!string.IsNullOrEmpty(dni))
        {
            ViewData["DNI"] = dni;
            tecnicos = tecnicos.Where(t => t.DNI == dni);
        }

        var tecnicosVM = tecnicos.Select(t => new TecnicoViewModel
        {
            Id = t.Id,
            Nombre = t.Nombre,
            Apellido = t.Apellido,
            DNI = t.DNI,
            Telefono = t.Telefono,
            ProveedorNombre = t.Proveedor.RazonSocial,
            Calificacion = t.Calificacion
        });

        return View(tecnicosVM);
    }

    public IActionResult Crear()
    {
        var proveedores = _context.Proveedores
            .Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.RazonSocial
            })
            .ToList();

        var tecnicoVM = new TecnicoViewModel
        {
            ProveedoresDisponibles = proveedores
        };

        return View(tecnicoVM);
    }

    public IActionResult Detalles(Guid id)
    {
        var tecnico = _context.Tecnicos
            .Include(t => t.Proveedor)
            .FirstOrDefault(t => t.Id == id);

        if (tecnico == null)
            return NotFound();

        var tecnicoVM = new TecnicoViewModel
        {
            Id = tecnico.Id,
            Nombre = tecnico.Nombre,
            Apellido = tecnico.Apellido,
            DNI = tecnico.DNI,
            Telefono = tecnico.Telefono,
            ProveedorNombre = tecnico.Proveedor.RazonSocial,
            Calificacion = tecnico.Calificacion
        };

        return View(tecnicoVM);
    }

    public IActionResult Editar(Guid id)
    {
        var tecnico = _context.Tecnicos
            .Include(t => t.Proveedor)
            .FirstOrDefault(t => t.Id == id);

        if (tecnico == null)
            return NotFound();

        var proveedores = _context.Proveedores
            .Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.RazonSocial
            })
            .ToList();

        var tecnicoVM = new TecnicoViewModel
        {
            Id = tecnico.Id,
            Nombre = tecnico.Nombre,
            Apellido = tecnico.Apellido,
            DNI = tecnico.DNI,
            Telefono = tecnico.Telefono,
            ProveedorId = tecnico.Proveedor.Id,
            ProveedoresDisponibles = proveedores
        };

        return View(tecnicoVM);
    }

    public IActionResult Eliminar(Guid id)
    {
        var tecnico = _context.Tecnicos
            .Include(t => t.Proveedor)
            .FirstOrDefault(t => t.Id == id);

        if (tecnico == null)
            return NotFound();

        var tecnicoVM = new TecnicoViewModel
        {
            Id = tecnico.Id,
            Nombre = tecnico.Nombre,
            Apellido = tecnico.Apellido,
            DNI = tecnico.DNI,
            Telefono = tecnico.Telefono,
            ProveedorNombre = tecnico.Proveedor.RazonSocial
        };

        return View(tecnicoVM);
    }

    #region AJAX_CALLS
    [HttpGet]
    public async Task<IActionResult> BuscarTecnicoPorDni(string dni)
    {
        var tecnico = await _context.Tecnicos
            .Include(t => t.Proveedor)
            .Include(t => t.Proveedor.Equipos)
            .FirstOrDefaultAsync(t => t.DNI == dni);

        if (tecnico == null)
            return Json(null);

        var equipos = tecnico.Proveedor.Equipos
            .Select(e => new { id = e.Id, nombre = e.Nombre })
            .ToList();

        return Json(new
        {
            tecnico = new
            {
                id = tecnico.Id,
                nombre = tecnico.Nombre,
                apellido = tecnico.Apellido,
                proveedor = tecnico.Proveedor.RazonSocial
            },
            equipos
        });
    }
    #endregion
}
