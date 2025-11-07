using Infraestructura.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentacion.Models;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var mejoresProveedores = await _context.Proveedores
            .OrderByDescending(p => p.Calificacion)
            .Take(5)
            .Select(p => new ProveedorRankingViewModel
            {
                RazonSocial = p.RazonSocial,
                Calificacion = p.Calificacion
            })
            .ToListAsync();

        var proveedoresConMasIngresos = await _context.Proveedores
            .Select(p => new ProveedorIngresosViewModel
            {
                RazonSocial = p.RazonSocial,
                TotalPagado =
                    p.Equipos.SelectMany(e => e.Adquisiciones).Sum(a => a.Costo) +
                    p.Tecnicos.SelectMany(t => t.Mantenimientos).Sum(m => m.Costo)
            })
            .OrderByDescending(p => p.TotalPagado)
            .Take(5)
            .ToListAsync();

        var totalProveedores = await _context.Proveedores.CountAsync();
        var totalAdquisiciones = await _context.Adquisiciones.CountAsync();
        var totalMantenimientos = await _context.Mantenimientos.CountAsync();
        var montoTotalInvertido = await _context.Adquisiciones.SumAsync(a => a.Costo)
            + await _context.Mantenimientos.SumAsync(m => m.Costo);

        var ultimasAdquisiciones = await _context.Adquisiciones
            .Include(a => a.Equipo).ThenInclude(e => e.Proveedor)
            .Include(a => a.Tecnico)
            .OrderByDescending(a => a.FechaAdquisicion)
            .Take(5)
            .Select(a => new AdquisicionRecienteViewModel
            {
                NumeroSerie = a.NumeroSerie,
                Equipo = a.Equipo.Nombre,
                Tecnico = a.Tecnico.Nombre + " " + a.Tecnico.Apellido,
                Proveedor = a.Equipo.Proveedor.RazonSocial,
                FechaAdquisicion = a.FechaAdquisicion,
                Costo = a.Costo
            })
            .ToListAsync();

        var ultimosMantenimientos = await _context.Mantenimientos
            .Include(m => m.Adquisicion).ThenInclude(a => a.Equipo)
            .Include(m => m.Tecnico)
            .Include(m => m.TipoMantenimiento)
            .OrderByDescending(m => m.Fecha)
            .Take(5)
            .Select(m => new MantenimientoRecienteViewModel
            {
                NumeroSerie = m.Adquisicion.NumeroSerie,
                Tecnico = m.Tecnico.Nombre + " " + m.Tecnico.Apellido,
                TipoMantenimiento = m.TipoMantenimiento.Descripcion,
                Fecha = m.Fecha,
                Costo = m.Costo,
                Estado = m.Estado.ToString()
            })
            .ToListAsync();

        var viewModel = new DashboardViewModel
        {
            MejoresProveedores = mejoresProveedores,
            ProveedoresConMasIngresos = proveedoresConMasIngresos,
            TotalProveedores = totalProveedores,
            TotalAdquisiciones = totalAdquisiciones,
            TotalMantenimientos = totalMantenimientos,
            MontoTotalInvertido = montoTotalInvertido,
            UltimasAdquisiciones = ultimasAdquisiciones,
            UltimosMantenimientos = ultimosMantenimientos
        };

        return View(viewModel);
    }

}
