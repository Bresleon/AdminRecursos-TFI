using Aplicacion.Interfaces.Repositorios;
using Dominio.Entidades;
using Infraestructura.Datos;

namespace Infraestructura.Repositorios;

public class ProveedorRepositorio : Repositorio<Proveedor>, IProveedorRepositorio
{
    private readonly ApplicationDbContext _context;

    public ProveedorRepositorio(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
