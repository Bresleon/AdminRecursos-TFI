using Dominio.Entidades;
using Infraestructura.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentacion.Models;

namespace Presentacion.Controllers;

public class ProductosController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductosController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Consultar()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Consultar(string numeroSerie)
    {
        var productoVisita = await ObtenerProducto(numeroSerie);

        if (productoVisita == null)
        {
            ViewBag.Mensaje = "No se encontró ningún producto con el número de serie proporcionado.";
            return View();
        }

        var productoVisitaVM = new ProductoConsultaViewModel
        {
            NumeroSerie = productoVisita.NumeroSerie,
            Nombre = productoVisita.Producto.Nombre,
            TipoProducto = productoVisita.Producto.TipoProducto.Nombre,
            FechaFinGarantia = productoVisita.FinGarantia,
            FechaAdquisicion = productoVisita.Visita.FechaHora,
            NombreCompletoTecnico = $"{productoVisita.Visita.Tecnico.Nombre} {productoVisita.Visita.Tecnico.Apellido}",
            DNITecnico = productoVisita.Visita.Tecnico.DNI
        };

        return View(productoVisitaVM);
    }

    private async Task<ProductoEnVisita?> ObtenerProducto(string numeroSerie)
    {
        return await _context.ProductosEnVisita
            .Include(pv => pv.Producto)
                .ThenInclude(p => p.TipoProducto)
            .Include(pv => pv.Visita)
                .ThenInclude(v => v.Tecnico)
            .Where(pv => pv.Concepto == Dominio.Enums.ConceptoAccion.VENTA)
            .FirstOrDefaultAsync(pv => pv.NumeroSerie == numeroSerie);
    }
}
