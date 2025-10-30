using Infraestructura.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentacion.Models;

namespace Presentacion.Controllers;

public class ProveedoresController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProveedoresController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string sortOrder)
    {
        ViewData["SortOrder"] = sortOrder ?? "";

        var proveedores = await _context.Proveedores.ToListAsync();

        var proveedoresVM = proveedores.Select(p => new ProveedorViewModel
        {
            Id = p.Id,
            RazonSocial = p.RazonSocial,
            CUIT = p.CUIT,
            MontoTotalPagado = CalcularMontoTotalPagado(p.Id),
            Calificacion = p.Calificacion
        }).ToList();

        proveedoresVM = sortOrder switch
        {
            "monto_desc" => proveedoresVM.OrderByDescending(p => p.MontoTotalPagado).ToList(),
            "monto_asc" => proveedoresVM.OrderBy(p => p.MontoTotalPagado).ToList(),
            "calificacion_desc" => proveedoresVM.OrderByDescending(p => p.Calificacion).ToList(),
            "calificacion_asc" => proveedoresVM.OrderBy(p => p.Calificacion).ToList(),
            _ => proveedoresVM.OrderBy(p => p.RazonSocial).ToList()
        };

        return View(proveedoresVM);
    }

    private decimal CalcularMontoTotalPagado(Guid proveedorId)
    {
        return _context.Tecnicos.Where(t => t.ProveedorId == proveedorId)
            .Include(t => t.Visitas)
            .SelectMany(t => t.Visitas)
            .Sum(v => v.MontoTotal);
    }
}
