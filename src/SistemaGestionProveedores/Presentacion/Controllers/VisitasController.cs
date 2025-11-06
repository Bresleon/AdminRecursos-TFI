//using Dominio.Enums;
//using Infraestructura.Datos;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Presentacion.Models;

//namespace Presentacion.Controllers;

//public class VisitasController : Controller
//{
//    private readonly ApplicationDbContext _context;

//    public VisitasController(ApplicationDbContext context)
//    {
//        _context = context;
//    }

//    public async Task<IActionResult> Index()
//    {
//        var visitasPendientes = await _context.Visitas
//            .Include(v => v.Tecnico)
//            .Where(v => v.Estado == Estado.PENDIENTE)
//            .ToListAsync();

//        return View(visitasPendientes);
//    }


//    public IActionResult Create()
//    {
//        var model = new VisitaCreateViewModel
//        {
//            Tecnicos = _context.Tecnicos
//                .Select(t => new SelectListItem
//                {
//                    Value = t.Id.ToString(),
//                    Text = t.Nombre
//                })
//                .ToList(),

//            ProductosVisita = new List<ProductoVisitaViewModel>()
//        };

//        return View(model);
//    }


//    public IActionResult Edit()
//    {
//        return View();
//    }

//    public IActionResult Delete()
//    {
//        return View();
//    }

//    #region AJAX_CALLS

//    [HttpGet]
//    public JsonResult ObtenerTecnicoPorDni(string dni)
//    {
//        if (string.IsNullOrWhiteSpace(dni))
//            return Json(new { success = false, message = "Debe ingresar un DNI." });

//        var tecnico = _context.Tecnicos
//            .Include(t => t.Proveedor)
//            .FirstOrDefault(t => t.DNI == dni);

//        if (tecnico == null)
//            return Json(new { success = false, message = "No se encontró un técnico con ese DNI." });

//        var productos = _context.Productos
//            .Where(p => p.ProveedorId == tecnico.ProveedorId)
//            .Select(p => new
//            {
//                id = p.Id,
//                nombre = p.Nombre
//            })
//            .ToList();

//        return Json(new
//        {
//            success = true,
//            tecnicoId = tecnico.Id,
//            tecnicoNombre = $"{tecnico.Nombre} {tecnico.Apellido}",
//            proveedor = tecnico.Proveedor.RazonSocial,
//            productos
//        });
//    }


//    [HttpGet]
//    public JsonResult ObtenerProductosPorTecnico(Guid tecnicoId)
//    {
//        var tecnico = _context.Tecnicos
//            .Include(t => t.Proveedor)
//            .FirstOrDefault(t => t.Id == tecnicoId);

//        if (tecnico == null)
//            return Json(new { productos = new List<object>() });

//        var productos = _context.Productos
//            .Where(p => p.ProveedorId == tecnico.ProveedorId)
//            .Select(p => new
//            {
//                id = p.Id,
//                nombre = p.Nombre
//            })
//            .ToList();

//        return Json(new { productos });
//    }

//    #endregion
//}
