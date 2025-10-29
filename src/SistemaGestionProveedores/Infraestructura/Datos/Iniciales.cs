using Dominio.Entidades;
using Dominio.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Datos;

public class Iniciales
{
    private static Iniciales? _instance = null;
    private static readonly object _lock = new();

    private Iniciales()
    {
        Inicializar();
    }

    public static Iniciales Instance
    {
        get
        {
            if (_instance is null)
            {
                lock (_lock)
                {
                    _instance = new();
                }
            }

            return _instance;
        }
    }

    public List<Proveedor> Proveedores { get; private set; } = new();
    public List<Producto> Productos { get; private set; } = new();
    public List<TipoProducto> TiposProducto { get; private set; } = new();
    public List<Tecnico> Tecnicos { get; private set; } = new();
    public List<Visita> Visitas { get; private set; } = new();
    public List<ProductoEnVisita> ProductosEnVisita { get; private set; } = new();

    private void Inicializar()
    {
        InicializarProveedores();
        InicializarTiposProducto();
        InicializarProductos();
        InicializarTecnicos();
        InicializarVisitas();
        InicializarProductosEnVisita();
    }

    private void InicializarProveedores()
    {
        Proveedores.AddRange(
        [
            new Proveedor
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                RazonSocial = "TechWorld S.A.",
                CUIT = "30-65432198-9",
                Email = "contacto@techworld.com",
                Direccion = "Av. Corrientes 2400, CABA",
                Telefono = "011-4785-5566",
                Calificacion = 0
            },
            new Proveedor
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                RazonSocial = "PCMax SRL",
                CUIT = "30-74569852-4",
                Email = "ventas@pcmax.com",
                Direccion = "Av. Rivadavia 9850, CABA",
                Telefono = "011-4678-1133",
                Calificacion = 0
            }
        ]);
    }

    private void InicializarTiposProducto()
    {
        TiposProducto.AddRange(
        [
            new TipoProducto
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Nombre = "Hardware"
            },
            new TipoProducto
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Nombre = "Periféricos"
            },
            new TipoProducto
            {
                Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                Nombre = "Software"
            },
            new TipoProducto
            {
                Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                Nombre = "Redes"
            },
        ]);
    }

    private void InicializarProductos()
    {
        Productos.AddRange(
        [
            new Producto
            {
                Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                Nombre = "Notebook Lenovo ThinkPad E14",
                ProveedorId = Proveedores.ElementAt(0).Id,
                TipoProductoId = TiposProducto.ElementAt(0).Id
            },
            new Producto
            {
                Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                Nombre = "Mouse Logitech M720",
                ProveedorId = Proveedores.ElementAt(0).Id,
                TipoProductoId = TiposProducto.ElementAt(1).Id
            },
            new Producto
            {
                Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                Nombre = "Router TP-Link Archer AX73",
                ProveedorId = Proveedores.ElementAt(1).Id,
                TipoProductoId = TiposProducto.ElementAt(3).Id
            },
            new Producto
            {
                Id = Guid.Parse("10101010-1010-1010-1010-101010101010"),
                Nombre = "Licencia Microsoft Office 365",
                ProveedorId = Proveedores.ElementAt(1).Id,
                TipoProductoId = TiposProducto.ElementAt(2).Id
            },
        ]);
    }

    private void InicializarTecnicos()
    {
        Tecnicos.AddRange(
        [
            new Tecnico
            {
                Id = Guid.Parse("3cda3b5b-3c70-48c8-bc95-aafdcd88601c"),
                ProveedorId = Proveedores.ElementAt(0).Id,
                Nombre = "Javier",
                Apellido = "Giménez",
                DNI = "33244567",
                Telefono = "+543814445566"
            },
            new Tecnico
            {
                Id = Guid.Parse("752b6aa3-4448-47eb-9edf-64c7b2fdcd02"),
                ProveedorId = Proveedores.ElementAt(1).Id,
                Nombre = "Lucía",
                Apellido = "Ríos",
                DNI = "37456789",
                Telefono = "+543814567891"
            },
            new Tecnico
            {
                Id = Guid.Parse("ee459fbd-509f-43cb-8068-f8c2047e03f0"),
                ProveedorId = Proveedores.ElementAt(1).Id,
                Nombre = "Martín",
                Apellido = "Coronel",
                DNI = "40899877",
                Telefono = "+543817894561"
            }
        ]);
    }

    private void InicializarVisitas()
    {
        Visitas.AddRange(
        [
            new Visita
            {
                Id = Guid.Parse("7c835130-a8e0-46cc-8aeb-8198a76d7222"),
                TecnicoId = Tecnicos.ElementAt(0).Id,
                MontoTotal = 165000,
                FechaHora = new DateTime(2025,10,15,14,30,0),
                Observaciones = "Adquisición de Notebook Lenovo y Mouse Logitech",
                Calificacion = 5
            },
            new Visita
            {
                Id = Guid.Parse("7c89a11d-8d06-4608-90f9-4bd1d6d2487e"),
                TecnicoId = Tecnicos.ElementAt(1).Id,
                MontoTotal = 70000,
                FechaHora = new DateTime(2025,10,20,17,15,0),
                Observaciones = "Adquisición de Router Tp-Link",
                Calificacion = 4
            },
            new Visita
            {
                Id = Guid.Parse("81a840f4-d3cf-49a1-959f-dcc3e5fbe9cf"),
                TecnicoId = Tecnicos.ElementAt(2).Id,
                MontoTotal = 180000,
                FechaHora = new DateTime(2025,10,23,11,47,0),
                Observaciones = "Adquisición de Paquete Microsoft Office",
                Calificacion = 5
            }
        ]);
    }

    private void InicializarProductosEnVisita()
    {
        ProductosEnVisita.AddRange(
        [
            new ProductoEnVisita
            {
                Id = Guid.Parse("67a48d6c-e3d1-4aa5-bcf8-75f6404a8c8a"),
                ProductoId = Productos.ElementAt(0).Id,
                VisitaId = Visitas.ElementAt(0).Id,
                NumeroSerie = "SN-LEN-12345",
                PrecioUnitario = 130000,
                FinGarantia = new DateOnly(2027,10,15),
                Concepto = ConceptoAccion.VENTA
            },
            new ProductoEnVisita
            {
                Id = Guid.Parse("cfc3972f-981a-4c8e-94a3-cce84d90785e"),
                ProductoId = Productos.ElementAt(1).Id,
                VisitaId = Visitas.ElementAt(0).Id,
                NumeroSerie = "SN-LOG-45678",
                PrecioUnitario = 35000,
                FinGarantia = new DateOnly(2026,10,15),
                Concepto = ConceptoAccion.VENTA
            },
            new ProductoEnVisita
            {
                Id = Guid.Parse("d9b12292-133a-48e5-97e1-fcf2eb39b38d"),
                ProductoId = Productos.ElementAt(2).Id,
                VisitaId = Visitas.ElementAt(1).Id,
                NumeroSerie = "SN-TPL-98765",
                PrecioUnitario = 70000,
                FinGarantia = new DateOnly(2028,10,20),
                Concepto = ConceptoAccion.VENTA
            },
            new ProductoEnVisita
            {
                Id = Guid.Parse("e0c65170-653d-4162-83bc-81bce59be1b4"),
                ProductoId = Productos.ElementAt(3).Id,
                VisitaId = Visitas.ElementAt(2).Id,
                NumeroSerie = "SN-MS-11223",
                PrecioUnitario = 180000,
                FinGarantia = new DateOnly(2026,10,23),
                Concepto = ConceptoAccion.VENTA
            }
        ]);
    }
}
