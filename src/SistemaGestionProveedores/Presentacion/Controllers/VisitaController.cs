using Dominio.Enums;
using Infraestructura.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentacion.Controllers;

public class VisitasController : Controller
{
    private readonly ApplicationDbContext _context;

    public VisitasController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var visitasPendientes = await _context.Visitas
            .Include(v => v.Tecnico)
            .Where(v => v.Estado == EstadoVisita.PENDIENTE)
            .ToListAsync();

        return View(visitasPendientes);
    }


    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Edit()
    {
        return View();
    }

    public IActionResult Delete()
    {
        return View();
    }
}
