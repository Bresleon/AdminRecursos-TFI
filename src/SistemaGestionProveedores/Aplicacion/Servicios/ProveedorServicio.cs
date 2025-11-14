using Aplicacion.Interfaces.Repositorios;
using Aplicacion.Interfaces.Servicios;
using Dominio.Entidades;
using System.Linq.Expressions;

namespace Aplicacion.Servicios;

public class ProveedorServicio : IProveedorServicio
{
    private readonly IProveedorRepositorio _repo;

    public ProveedorServicio(IProveedorRepositorio repo)
    {
        _repo = repo;
    }

    public async Task Agregar(Proveedor proveedor)
    {
        var esExitosa = await _repo.Agregar(proveedor);

        if (!esExitosa)
            throw new Exception("No se pudo agregar el proveedor");
    }

    public async Task Eliminar(Proveedor proveedor)
    {
        proveedor.Activado = 0;
        var esExitosa = await _repo.Modificar(proveedor);

        if (!esExitosa)
            throw new Exception("No se pudo eliminar el proveedor");
    }

    public async Task Modificar(Proveedor proveedor)
    {
        var esExitosa = await _repo.Modificar(proveedor);

        if (!esExitosa)
            throw new Exception("No se pudo modificar el proveedor");
    }

    public async Task<Proveedor?> Obtener(Guid id)
    {
        var proveedor = await _repo.ObtenerPorId(id, p => p.Tecnicos);

        if (proveedor == null)
            throw new Exception("No se pudo encontrar el proveedor");

        return proveedor;
    }

    public async Task<IEnumerable<Proveedor>> ObtenerTodos(Expression<Func<Proveedor, bool>>? filtro = null)
    {
        return await (filtro == null ? _repo.ObtenerTodos() : _repo.ObtenerTodos(filtro));
    }
}
