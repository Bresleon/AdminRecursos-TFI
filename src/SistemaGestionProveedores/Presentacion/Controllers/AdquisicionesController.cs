using Dominio.Entidades;
using Infraestructura.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Presentacion.Models;

namespace Presentacion.Controllers;

public class AdquisicionesController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdquisicionesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var adquisiciones = _context.Adquisiciones
            .Include(a => a.Tecnico)
            .Include(a => a.Equipo)
                .ThenInclude(e => e.TipoEquipo)
            .ToList()
            .OrderByDescending(a => a.FechaAdquisicion);

        var adquisicionesVM = adquisiciones.Select(a => new AdquisicionViewModel
        {
            Id = a.Id,
            NumeroSerie = a.NumeroSerie,
            Nombre = a.Equipo.Nombre,
            TipoEquipo = a.Equipo.TipoEquipo.Nombre,
            Costo = a.Costo,
            FechaFinGarantia = a.FechaFinGarantia,
            FechaAdquisicion = a.FechaAdquisicion,
            NombreCompletoTecnico = $"{a.Tecnico.Nombre} {a.Tecnico.Apellido}",
            DNITecnico = a.Tecnico.DNI
        }).ToList();

        return View(adquisicionesVM);
    }

    public IActionResult Crear()
    {
        return View();
    }

    public IActionResult Consultar()
    {
        return View();
    }

    public async Task<IActionResult> Editar(Guid id)
    {
        var adquisicion = await ObtenerAdquisicion(id);

        var adquisicionEditVM = new AdquisicionUpsertViewModel
        {
            Id = adquisicion!.Id,
            TecnicoId = adquisicion.Tecnico.Id,
            EquipoId = adquisicion.Equipo.Id,
            NumeroSerie = adquisicion.NumeroSerie,
            FechaAdquisicion = adquisicion.FechaAdquisicion,
            FechaFinGarantia = adquisicion.FechaFinGarantia,
            Costo = adquisicion.Costo,
            NombreCompletoTecnico = $"{adquisicion.Tecnico.Nombre} {adquisicion.Tecnico.Apellido}",
            NombreProveedor = adquisicion.Tecnico.Proveedor.RazonSocial,
            EquiposDisponibles = adquisicion.Tecnico.Proveedor.Equipos
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nombre
                })
                .ToList()
        };

        return View(adquisicionEditVM);
    }

    [HttpPost]
    public async Task<IActionResult> Consultar(string numeroSerie)
    {
        var adquisicion = await ObtenerAdquisicion(numeroSerie);

        if (adquisicion == null)
        {
            ViewBag.Mensaje = "No se encontró ningún equipo con el número de serie proporcionado.";
            return View();
        }

        var adquisicionVM = new AdquisicionViewModel
        {
            NumeroSerie = adquisicion.NumeroSerie,
            Nombre = adquisicion.Equipo.Nombre,
            TipoEquipo = adquisicion.Equipo.TipoEquipo.Nombre,
            Costo = adquisicion.Costo,
            FechaFinGarantia = adquisicion.FechaFinGarantia,
            FechaAdquisicion = adquisicion.FechaAdquisicion,
            NombreCompletoTecnico = $"{adquisicion.Tecnico.Nombre} {adquisicion.Tecnico.Apellido}",
            DNITecnico = adquisicion.Tecnico.DNI
        };

        return View(adquisicionVM);
    }

    private async Task<Adquisicion?> ObtenerAdquisicion(Guid id)
    {
        return await _context.Adquisiciones
            .Include(a => a.Tecnico)
                .ThenInclude(t => t.Proveedor)
                    .ThenInclude(p => p.Equipos)
            .Include(a => a.Equipo)
                .ThenInclude(e => e.TipoEquipo)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    private async Task<Adquisicion?> ObtenerAdquisicion(string numeroSerie)
    {
        return await _context.Adquisiciones
            .Include(a => a.Tecnico)
            .Include(a => a.Equipo)
                .ThenInclude(e => e.TipoEquipo)
            .FirstOrDefaultAsync(a => a.NumeroSerie == numeroSerie);
    }
}
