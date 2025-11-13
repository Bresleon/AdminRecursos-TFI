using Aplicacion.Interfaces.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentacion.Models.Home;

[Authorize]
public class HomeController : Controller
{
    private readonly IProveedorServicio _proveedorServ;
    private readonly IAdquisicionServicio _adquisicionServ;
    private readonly IMantenimientoServicio _mantenimientoServ;

    public HomeController(IProveedorServicio proveedorServ, 
                          IAdquisicionServicio adquisicionServ, 
                          IMantenimientoServicio mantenimientoServ)
    {
        _proveedorServ = proveedorServ;
        _adquisicionServ = adquisicionServ;
        _mantenimientoServ = mantenimientoServ;
    }

    public async Task<IActionResult> Index()
    {
        var proveedores = await _proveedorServ.ObtenerTodos();
        var adquisiciones = await _adquisicionServ.ObtenerTodos();
        var mantenimientos = await _mantenimientoServ.ObtenerTodos();

        var mejoresProveedores = proveedores
            .OrderByDescending(p => p.Calificacion)
            .Take(5)
            .Select(p => new ProveedorRankingViewModel
            {
                RazonSocial = p.RazonSocial,
                Calificacion = p.Calificacion
            })
            .ToList();

        var proveedoresConMasIngresos = proveedores
            .Select(p => new ProveedorIngresosViewModel
            {
                RazonSocial = p.RazonSocial,
                TotalPagado =
                    p.Equipos.SelectMany(e => e.Adquisiciones).Sum(a => a.Costo) +
                    p.Tecnicos.SelectMany(t => t.Mantenimientos).Sum(m => m.Costo)
            })
            .OrderByDescending(p => p.TotalPagado)
            .Take(5)
            .ToList();

        var totalProveedores = proveedores.Count();
        var totalAdquisiciones = adquisiciones.Count();
        var totalMantenimientos = mantenimientos.Count();
        var montoTotalInvertido = adquisiciones.Sum(a => a.Costo) + mantenimientos.Sum(m => m.Costo);

        var ultimasAdquisiciones = adquisiciones
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
            .ToList();

        var ultimosMantenimientos = mantenimientos
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
            .ToList();

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
