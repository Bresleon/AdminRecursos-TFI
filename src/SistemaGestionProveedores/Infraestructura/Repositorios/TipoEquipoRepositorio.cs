using Aplicacion.Interfaces.Repositorios;
using Dominio.Entidades;
using Infraestructura.Datos;

namespace Infraestructura.Repositorios;

public class TipoEquipoRepositorio : Repositorio<TipoEquipo>, ITipoEquipoRepositorio
{
    private readonly ApplicationDbContext _context;

    public TipoEquipoRepositorio(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
