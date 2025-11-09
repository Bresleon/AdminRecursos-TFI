using Aplicacion.Interfaces.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentacion.Models.Tecnicos;

namespace Presentacion.Controllers;

public class TecnicosController : Controller
{
    private readonly ITecnicoServicio _tecnicoServ;
    private readonly IProveedorServicio _proveedorServ;

    public TecnicosController(ITecnicoServicio tecnicoServ, IProveedorServicio proveedorServ)
    {
        _tecnicoServ = tecnicoServ;
        _proveedorServ = proveedorServ;
    }


    public async Task<IActionResult> Index(string? dni = null)
    {
        var tecnicos = !string.IsNullOrEmpty(dni) 
            ? await _tecnicoServ.ObtenerTodos(t => t.DNI == dni)
            : await _tecnicoServ.ObtenerTodos();

        ViewData["DNI"] = dni;

        var tecnicosVM = tecnicos.Select(t => new TecnicoViewModel
        {
            Id = t.Id,
            Nombre = t.Nombre,
            Apellido = t.Apellido,
            DNI = t.DNI,
            Telefono = t.Telefono,
            ProveedorNombre = t.Proveedor.RazonSocial,
            Calificacion = t.Calificacion
        });

        return View(tecnicosVM);
    }

    public async Task<IActionResult> Crear()
    {
        var proveedores = await _proveedorServ.ObtenerTodos();

        var proveedoresSelect = proveedores.Select(p => new SelectListItem
        {
            Value = p.Id.ToString(),
            Text = p.RazonSocial
        }).ToList();

        var tecnicoVM = new TecnicoViewModel
        {
            ProveedoresDisponibles = proveedoresSelect
        };

        return View(tecnicoVM);
    }

    public async Task<IActionResult> Detalles(Guid id)
    {
        var tecnico = await _tecnicoServ.Obtener(id);

        if (tecnico == null)
            return NotFound();

        var tecnicoVM = new TecnicoViewModel
        {
            Id = tecnico.Id,
            Nombre = tecnico.Nombre,
            Apellido = tecnico.Apellido,
            DNI = tecnico.DNI,
            Telefono = tecnico.Telefono,
            ProveedorNombre = tecnico.Proveedor.RazonSocial,
            Calificacion = tecnico.Calificacion
        };

        return View(tecnicoVM);
    }

    public async Task<IActionResult> Editar(Guid id)
    {
        var tecnico = await _tecnicoServ.Obtener(id);

        if (tecnico == null)
            return NotFound();

        var proveedores = await _proveedorServ.ObtenerTodos();

        var proveedoresSelect = proveedores.Select(p => new SelectListItem
        {
            Value = p.Id.ToString(),
            Text = p.RazonSocial
        }).ToList();

        var tecnicoVM = new TecnicoViewModel
        {
            Id = tecnico.Id,
            Nombre = tecnico.Nombre,
            Apellido = tecnico.Apellido,
            DNI = tecnico.DNI,
            Telefono = tecnico.Telefono,
            ProveedorId = tecnico.Proveedor.Id,
            ProveedoresDisponibles = proveedoresSelect
        };

        return View(tecnicoVM);
    }

    public async Task<IActionResult> Eliminar(Guid id)
    {
        var tecnico = await _tecnicoServ.Obtener(id);

        if (tecnico == null)
            return NotFound();

        var tecnicoVM = new TecnicoViewModel
        {
            Id = tecnico.Id,
            Nombre = tecnico.Nombre,
            Apellido = tecnico.Apellido,
            DNI = tecnico.DNI,
            Telefono = tecnico.Telefono,
            ProveedorNombre = tecnico.Proveedor.RazonSocial
        };

        return View(tecnicoVM);
    }

    #region AJAX_CALLS
    [HttpGet]
    public async Task<IActionResult> BuscarTecnicoPorDni(string dni)
    {
        var tecnico = await _tecnicoServ.Obtener(dni);

        if (tecnico == null)
            return Json(null);

        var equipos = tecnico.Proveedor.Equipos
            .Select(e => new { id = e.Id, nombre = e.Nombre })
            .ToList();

        return Json(new
        {
            tecnico = new
            {
                id = tecnico.Id,
                nombre = tecnico.Nombre,
                apellido = tecnico.Apellido,
                proveedor = tecnico.Proveedor.RazonSocial
            },
            equipos
        });
    }
    #endregion
}
