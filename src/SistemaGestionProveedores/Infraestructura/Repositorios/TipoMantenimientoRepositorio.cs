using Aplicacion.Interfaces.Repositorios;
using Dominio.Entidades;
using Infraestructura.Datos;

namespace Infraestructura.Repositorios;

public class TipoMantenimientoRepositorio : Repositorio<TipoMantenimiento>, ITipoMantenimientoRepositorio
{
    private readonly ApplicationDbContext _context;

    public TipoMantenimientoRepositorio(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
