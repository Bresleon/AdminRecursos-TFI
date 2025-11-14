using Aplicacion.Servicios.Seguridad;
using Dominio.Entidades;
using Infraestructura.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentacion.Models.Account;

namespace Presentacion.Controllers;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly PasswordService _passwordService;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
        _passwordService = new PasswordService();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (model.Contrasena != model.ConfirmarContrasena)
        {
            ViewBag.Error = "Las contraseñas no coinciden.";
            return View(model);
        }

        var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == model.NombreUsuario);

        if (usuarioExistente != null)
        {
            ViewBag.Error = "El nombre de usuario ya existe.";
            return View(model);
        }

        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            NombreUsuario = model.NombreUsuario,
            Contrasena = _passwordService.Hashear(model.Contrasena),
            Rol = model.Rol
        };

        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Cuenta registrada correctamente.";
        return RedirectToAction("Login", "Auth");
    }

}
