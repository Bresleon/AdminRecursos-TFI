using Aplicacion.Interfaces.Repositorios;
using Dominio.Entidades;
using Infraestructura.Datos;

namespace Infraestructura.Repositorios;

public class TecnicoRepositorio : Repositorio<Tecnico>, ITecnicoRepositorio
{
    private readonly ApplicationDbContext _context;

    public TecnicoRepositorio(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
