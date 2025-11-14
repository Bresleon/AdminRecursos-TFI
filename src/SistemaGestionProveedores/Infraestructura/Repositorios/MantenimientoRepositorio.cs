using Aplicacion.Interfaces.Repositorios;
using Dominio.Entidades;
using Infraestructura.Datos;

namespace Infraestructura.Repositorios;

public class MantenimientoRepositorio : Repositorio<Mantenimiento>, IMantenimientoRepositorio
{
    private readonly ApplicationDbContext _context;

    public MantenimientoRepositorio(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
