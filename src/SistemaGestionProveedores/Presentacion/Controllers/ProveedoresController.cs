using Aplicacion.Interfaces.Servicios;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Presentacion.Models.Proveedores;
using System.Threading.Tasks;

namespace Presentacion.Controllers;

public class ProveedoresController : Controller
{
    private readonly IProveedorServicio _proveedorServ;
    private readonly IAdquisicionServicio _adquisicionServ;
    private readonly IMantenimientoServicio _mantenimientoServ;

    public ProveedoresController(IProveedorServicio proveedorServ, IAdquisicionServicio adquisicionServ, IMantenimientoServicio mantenimientoServ)
    {
        _proveedorServ = proveedorServ;
        _adquisicionServ = adquisicionServ;
        _mantenimientoServ = mantenimientoServ;
    }

    public async Task<IActionResult> Index(string sortOrder)
    {
        ViewData["SortOrder"] = sortOrder ?? "";

        var proveedores = await _proveedorServ.ObtenerTodos();

        var proveedoresVM = proveedores.Select(p => new ProveedorViewModel
        {
            Id = p.Id,
            RazonSocial = p.RazonSocial,
            CUIT = p.CUIT,
            MontoTotalPagado = CalcularMontoTotalPagado(p.Id).Result,
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

    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Crear(ProveedorViewModel modelo)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Uno o varios de los datos son incorrectos o están vacíos");
            return View(modelo);
        }

        var proveedor = new Proveedor
        {
            RazonSocial = modelo.RazonSocial,
            CUIT = modelo.CUIT,
            Email = modelo.Email,
            Direccion = modelo.Direccion,
            Telefono = modelo.Telefono,
        };

        try
        {
            await _proveedorServ.Agregar(proveedor);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", e.Message);
            return View(modelo);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Editar(Guid id)
    {
        var proveedor = await _proveedorServ.Obtener(id);

        if (proveedor == null)
        {
            return NotFound();
        }

        var proveedorVM = new ProveedorViewModel
        {
            Id = proveedor.Id,
            RazonSocial = proveedor.RazonSocial,
            CUIT = proveedor.CUIT,
            Email = proveedor.Email,
            Direccion = proveedor.Direccion,
            Telefono = proveedor.Telefono,
            Calificacion = proveedor.Calificacion
        };

        return View(proveedorVM);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(ProveedorViewModel modelo)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Uno o varios de los datos son incorrectos o están vacíos");
            return View(modelo);
        }

        var proveedor = new Proveedor
        {
            Id = modelo.Id,
            RazonSocial = modelo.RazonSocial,
            CUIT = modelo.CUIT,
            Email = modelo.Email,
            Direccion = modelo.Direccion,
            Telefono = modelo.Telefono,
            Calificacion = modelo.Calificacion,
        };

        try
        {
            await _proveedorServ.Modificar(proveedor);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", e.Message);
            return View(modelo);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Detalles(Guid id)
    {
        var proveedor = await _proveedorServ.Obtener(id);

        if (proveedor == null)
        {
            return NotFound();
        }

        var proveedorVM = new ProveedorViewModel
        {
            Id = proveedor.Id,
            RazonSocial = proveedor.RazonSocial,
            CUIT = proveedor.CUIT,
            Email = proveedor.Email,
            Direccion = proveedor.Direccion,
            Telefono = proveedor.Telefono,
            Calificacion = proveedor.Calificacion,
            MontoTotalPagado = CalcularMontoTotalPagado(proveedor.Id).Result
        };

        return View(proveedorVM);
    }

    public async Task<IActionResult> Eliminar(Guid id)
    {
        var proveedor = await _proveedorServ.Obtener(id);

        if (proveedor == null)
        {
            return NotFound();
        }

        var proveedorVM = new ProveedorViewModel
        {
            Id = proveedor.Id,
            RazonSocial = proveedor.RazonSocial,
            CUIT = proveedor.CUIT,
            Email = proveedor.Email,
            Direccion = proveedor.Direccion,
            Telefono = proveedor.Telefono,
            Calificacion = proveedor.Calificacion
        };

        return View(proveedorVM);
    }

    [HttpPost]
    public async Task<IActionResult> Eliminar(ProveedorViewModel modelo)
    {
        var proveedor = new Proveedor { Id = modelo.Id };

        try
        {
            await _proveedorServ.Eliminar(proveedor);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", e.Message);
            return View(modelo);
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<decimal> CalcularMontoTotalPagado(Guid proveedorId)
    {
        var montoTotalAdquisiciones = (await _adquisicionServ.ObtenerTodos(a => a.Tecnico.ProveedorId == proveedorId)).Sum(a => a.Costo);
        var montoTotalMantenimientos = (await _mantenimientoServ.ObtenerTodos(m => m.Tecnico.ProveedorId == proveedorId)).Sum(m => m.Costo);

        return montoTotalAdquisiciones + montoTotalMantenimientos;
    }
}
