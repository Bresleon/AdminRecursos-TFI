using Aplicacion.Interfaces.Repositorios;
using Dominio.Entidades;
using Infraestructura.Datos;

namespace Infraestructura.Repositorios;

public class EquipoRepositorio : Repositorio<Equipo>, IEquipoRepositorio
{
    private readonly ApplicationDbContext _context;

    public EquipoRepositorio(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
