using Infraestructura.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        var tecnicosVM = tecnicos.Select(t => new Models.TecnicoViewModel
        {
            Id = t.Id,
            NombreCompleto = t.Nombre + " " + t.Apellido,
            DNI = t.DNI,
            Telefono = t.Telefono,
            Proveedor = t.Proveedor.RazonSocial,
            Calificacion = t.Calificacion
        });

        return View(tecnicosVM);
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
