using Aplicacion.Interfaces.Servicios;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentacion.Models.Adquisiciones;
using Presentacion.Models.Tecnicos;
using System.Threading.Tasks;

namespace Presentacion.Controllers;

public class AdquisicionesController : Controller
{
    private readonly IAdquisicionServicio _servicio;

    public AdquisicionesController(IAdquisicionServicio servicio)
    {
        _servicio = servicio;
    }

    public async Task<IActionResult> Index()
    {
        var adquisiciones = await _servicio.ObtenerTodos();

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

    [HttpPost]
    public async Task<IActionResult> Crear(AdquisicionUpsertViewModel modelo)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Uno o varios de los datos son incorrectos o están vacíos");
            return View(modelo);
        }

        if (modelo.Costo <= 0)
        {
            ModelState.AddModelError("Costo", "El costo de la adquisición debe ser mayor a cero");
            return View(modelo);
        }

        var adquisicion = new Adquisicion
        {
            EquipoId = modelo.EquipoId,
            TecnicoId = modelo.TecnicoId,
            NumeroSerie = modelo.NumeroSerie,
            FechaAdquisicion = modelo.FechaAdquisicion,
            FechaFinGarantia = modelo.FechaFinGarantia,
            Costo = modelo.Costo,
        };

        try
        {
            await _servicio.Agregar(adquisicion);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", e.Message);
            return View(modelo);
        }

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Consultar()
    {
        return View();
    }

    public async Task<IActionResult> Editar(Guid id)
    {
        var adquisicion = await _servicio.Obtener(id);

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
            EquiposDisponibles = await ObtenerEquiposDisponiblesSelect(id)
        };

        return View(adquisicionEditVM);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(AdquisicionUpsertViewModel modelo)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Uno o varios de los datos son incorrectos o están vacíos");
            modelo.EquiposDisponibles = await ObtenerEquiposDisponiblesSelect(modelo.Id);
            return View(modelo);
        }

        if (modelo.Costo <= 0)
        {
            ModelState.AddModelError("Costo", "El costo de la adquisición debe ser mayor a cero");
            modelo.EquiposDisponibles = await ObtenerEquiposDisponiblesSelect(modelo.Id);
            return View(modelo);
        }

        var adquisicion = new Adquisicion
        {
            Id = modelo.Id,
            EquipoId = modelo.EquipoId,
            TecnicoId = modelo.TecnicoId,
            NumeroSerie = modelo.NumeroSerie,
            FechaAdquisicion = modelo.FechaAdquisicion,
            FechaFinGarantia = modelo.FechaFinGarantia,
            Costo = modelo.Costo,
        };

        try
        {
            await _servicio.Modificar(adquisicion);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", e.Message);
            return View(modelo);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Consultar(string numeroSerie)
    {
        var adquisicion = await _servicio.Obtener(numeroSerie);

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

    private async Task<List<SelectListItem>> ObtenerEquiposDisponiblesSelect(Guid adquisicionId)
    {
        var adquisicion = await _servicio.Obtener(adquisicionId);

        return adquisicion!.Tecnico.Proveedor.Equipos
            .Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Nombre
            })
            .ToList();
    }
}
