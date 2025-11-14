using Aplicacion.Interfaces.Repositorios;
using Dominio.Entidades;
using Infraestructura.Datos;

namespace Infraestructura.Repositorios;

public class AdquisicionRepositorio : Repositorio<Adquisicion>, IAdquisicionRepositorio
{
    private readonly ApplicationDbContext _context;

    public AdquisicionRepositorio(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
