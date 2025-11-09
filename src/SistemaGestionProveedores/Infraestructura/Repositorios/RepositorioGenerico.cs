using Aplicacion.Interfaces.Repositorios;
using Infraestructura.Datos;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infraestructura.Repositorios;

public class Repositorio<T> : IRepositorio<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repositorio(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<bool> Agregar(T entidad)
    {
        await _dbSet.AddAsync(entidad);
        return await GuardarCambiosAsync();
    }

    public async Task<bool> Eliminar(T entidad)
    {
        _dbSet.Remove(entidad);
        return await GuardarCambiosAsync();
    }

    public async Task<bool> Modificar(T entidad)
    {
        _dbSet.Update(entidad);
        return await GuardarCambiosAsync();
    }

    public async Task<T?> Obtener(Expression<Func<T, bool>> filtro,
                                  params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        if (includes != null)
        {
            foreach (var include in includes)
                query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync(filtro);
    }

    public async Task<T?> ObtenerPorId(Guid id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        if (includes != null)
        {
            foreach (var include in includes)
                query = query.Include(include);
        }

        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> ObtenerTodos(
        Expression<Func<T, bool>>? filtro = null,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        if (includes != null)
        {
            foreach (var include in includes)
                query = query.Include(include);
        }

        if (filtro != null)
            query = query.Where(filtro);

        return await query.ToListAsync();
    }

    private async Task<bool> GuardarCambiosAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}