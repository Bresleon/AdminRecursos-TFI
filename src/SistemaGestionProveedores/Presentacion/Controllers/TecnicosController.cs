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
}
