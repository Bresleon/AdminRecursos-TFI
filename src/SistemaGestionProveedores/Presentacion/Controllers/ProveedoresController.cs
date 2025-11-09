using Aplicacion.Interfaces.Servicios;
using Microsoft.AspNetCore.Mvc;
using Presentacion.Models.Proveedores;

namespace Presentacion.Controllers;

public class ProveedoresController : Controller
{
    private readonly IProveedorServicio _servicio;

    public ProveedoresController(IProveedorServicio servicio)
    {
        _servicio = servicio;
    }

    public async Task<IActionResult> Index(string sortOrder)
    {
        ViewData["SortOrder"] = sortOrder ?? "";

        var proveedores = await _servicio.ObtenerTodos();

        var proveedoresVM = proveedores.Select(p => new ProveedorViewModel
        {
            Id = p.Id,
            RazonSocial = p.RazonSocial,
            CUIT = p.CUIT,
            //MontoTotalPagado = CalcularMontoTotalPagado(p.Id),
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

    public async Task<IActionResult> Editar(Guid id)
    {
        var proveedor = await _servicio.Obtener(id);

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
        };

        return View(proveedorVM);
    }

    public async Task<IActionResult> Detalles(Guid id)
    {
        var proveedor = await _servicio.Obtener(id);

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
            //MontoTotalPagado = CalcularMontoTotalPagado(proveedor.Id)
        };

        return View(proveedorVM);
    }

    public async Task<IActionResult> Eliminar(Guid id)
    {
        var proveedor = await _servicio.Obtener(id);

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
        };

        return View(proveedorVM);
    }

    // TODO: Incluir uso de AdquisicionServicio y MantenimientoServicio
    //private decimal CalcularMontoTotalPagado(Guid proveedorId)
    //{
    //    var montoTotalAdquisiciones = _context.Adquisiciones
    //        .Include(a => a.Tecnico)
    //            .ThenInclude(t => t.Proveedor)
    //        .Where(a => a.Tecnico.ProveedorId == proveedorId)
    //        .Sum(a => a.Costo);

    //    var montoTotalMantenimientos = _context.Mantenimientos
    //        .Include(m => m.Tecnico)
    //            .ThenInclude(t => t.Proveedor)
    //        .Where(m => m.Tecnico.ProveedorId == proveedorId)
    //        .Sum(m => m.Costo);

    //    return montoTotalAdquisiciones + montoTotalMantenimientos;
    //}
}
