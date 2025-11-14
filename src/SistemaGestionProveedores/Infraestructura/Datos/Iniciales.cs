using Aplicacion.Servicios.Seguridad;
using Dominio.Entidades;
using Dominio.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Datos;

public class Iniciales
{
    private static Iniciales? _instance = null;
    private static readonly object _lock = new();
    private static PasswordService _passwordService = new();

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
    public List<Equipo> Equipos { get; private set; } = new();
    public List<TipoEquipo> TiposEquipo { get; private set; } = new();
    public List<Tecnico> Tecnicos { get; private set; } = new();
    public List<Adquisicion> Adquisiciones { get; private set; } = new();
    public List<TipoMantenimiento> TiposMantenimiento { get; private set; } = new();
    public List<Mantenimiento> Mantenimientos { get; private set; } = new();
    public List<Usuario> Usuarios { get; private set; } = new();

    private void Inicializar()
    {
        InicializarProveedores();
        InicializarTiposEquipo();
        InicializarEquipos();
        InicializarTecnicos();
        InicializarAdquisiciones();
        InicializarTiposMantenimiento();
        InicializarMantenimientos();
        InicializarUsuarios();
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
                Calificacion = 4.25f
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

    private void InicializarTiposEquipo()
    {
        TiposEquipo.AddRange(
        [
            new TipoEquipo
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Nombre = "Hardware"
            },
            new TipoEquipo
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Nombre = "Periféricos"
            },
            new TipoEquipo
            {
                Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                Nombre = "Software"
            },
            new TipoEquipo
            {
                Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                Nombre = "Redes"
            },
        ]);
    }

    private void InicializarEquipos()
    {
        Equipos.AddRange(
        [
            new Equipo
            {
                Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                Nombre = "Notebook Lenovo ThinkPad E14",
                ProveedorId = Proveedores.ElementAt(0).Id,
                TipoEquipoId = TiposEquipo.ElementAt(0).Id
            },
            new Equipo
            {
                Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                Nombre = "Mouse Logitech M720",
                ProveedorId = Proveedores.ElementAt(0).Id,
                TipoEquipoId = TiposEquipo.ElementAt(1).Id
            },
            new Equipo
            {
                Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                Nombre = "Router TP-Link Archer AX73",
                ProveedorId = Proveedores.ElementAt(1).Id,
                TipoEquipoId = TiposEquipo.ElementAt(3).Id
            },
            new Equipo
            {
                Id = Guid.Parse("10101010-1010-1010-1010-101010101010"),
                Nombre = "Licencia Microsoft Office 365",
                ProveedorId = Proveedores.ElementAt(1).Id,
                TipoEquipoId = TiposEquipo.ElementAt(2).Id
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
                Telefono = "+543814445566",
                Calificacion = 4.5f
            },
            new Tecnico
            {
                Id = Guid.Parse("752b6aa3-4448-47eb-9edf-64c7b2fdcd02"),
                ProveedorId = Proveedores.ElementAt(0).Id,
                Nombre = "Lucía",
                Apellido = "Ríos",
                DNI = "37456789",
                Telefono = "+543814567891",
                Calificacion = 4.0f
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

    private void InicializarAdquisiciones()
    {
        Adquisiciones.AddRange(
        [
            new Adquisicion
            {
                Id = Guid.Parse("67a48d6c-e3d1-4aa5-bcf8-75f6404a8c8a"),
                EquipoId = Equipos.ElementAt(0).Id,
                TecnicoId = Tecnicos.ElementAt(0).Id,
                NumeroSerie = "SN-LEN-12345",
                Costo = 130000,
                FechaFinGarantia = new DateOnly(2027,10,15),
                FechaAdquisicion = new DateOnly(2025,10,15),
            },
            new Adquisicion
            {
                Id = Guid.Parse("cfc3972f-981a-4c8e-94a3-cce84d90785e"),
                EquipoId = Equipos.ElementAt(1).Id,
                TecnicoId = Tecnicos.ElementAt(0).Id,
                NumeroSerie = "SN-LOG-45678",
                Costo = 35000,
                FechaFinGarantia = new DateOnly(2026,10,15),
                FechaAdquisicion = new DateOnly(2025,10,15),
            },
            new Adquisicion
            {
                Id = Guid.Parse("d9b12292-133a-48e5-97e1-fcf2eb39b38d"),
                EquipoId = Equipos.ElementAt(2).Id,
                TecnicoId = Tecnicos.ElementAt(0).Id,
                NumeroSerie = "SN-TPL-98765",
                Costo = 70000,
                FechaFinGarantia = new DateOnly(2028,10,20),
                FechaAdquisicion = new DateOnly(2025,10,20),
            },
            new Adquisicion
            {
                Id = Guid.Parse("e0c65170-653d-4162-83bc-81bce59be1b4"),
                EquipoId = Equipos.ElementAt(3).Id,
                TecnicoId = Tecnicos.ElementAt(0).Id,
                NumeroSerie = "SN-MS-11223",
                Costo = 180000,
                FechaFinGarantia = new DateOnly(2026,10,23),
                FechaAdquisicion = new DateOnly(2025,10,23),
            }
        ]);
    }

    private void InicializarTiposMantenimiento()
    {
        TiposMantenimiento.AddRange(
        [
            new TipoMantenimiento
            {
                Id = Guid.Parse("7F900D5C-45B5-424E-9A4C-7663C9ACCE1E"),
                Descripcion = "Instalación"
            },
            new TipoMantenimiento
            {
                Id = Guid.Parse("1B74F1E6-18A3-40A7-BD25-279F97329661"),
                Descripcion = "Reparación en garantía"
            },
            new TipoMantenimiento
            {
                Id = Guid.Parse("97E99A3B-E5A1-4AC4-9D60-EAC409C3D681"),
                Descripcion = "Reparación fuera de garantía"
            },
            new TipoMantenimiento
            {
                Id = Guid.Parse("9DE506E3-E483-4CE1-B44E-9D66AC847F8C"),
                Descripcion = "Actualización"
            }
        ]);
    }

    private void InicializarMantenimientos()
    {
        Mantenimientos.AddRange(
        [
            new Mantenimiento
            {
                Id = Guid.Parse("CF58E1E4-CFC9-4DC4-8066-C82C0C62181D"),
                AdquisicionId = Adquisiciones.ElementAt(0).Id,
                TecnicoId = Tecnicos.ElementAt(0).Id,
                TipoMantenimientoId = TiposMantenimiento.ElementAt(0).Id,
                Estado = Estado.FINALIZADO,
                Fecha = new DateOnly(2025,11,1),
                Costo = 0,
                Descripcion = "Instalación inicial del sistema operativo y software básico.",
                Calificacion = 4.5f
            },
            new Mantenimiento
            {
                Id = Guid.Parse("9F0E8550-0C39-4CF2-8255-852C72FE9AAB"),
                AdquisicionId = Adquisiciones.ElementAt(2).Id,
                TecnicoId = Tecnicos.ElementAt(1).Id,
                TipoMantenimientoId = TiposMantenimiento.ElementAt(3).Id,
                Estado = Estado.FINALIZADO,
                Fecha = new DateOnly(2026,11,27),
                Costo = 15000,
                Descripcion = "Actualización del firmware del router para mejorar la seguridad.",
                Calificacion = 4.0f
            }
        ]);
    }

    private void InicializarUsuarios()
    {
        Usuarios.AddRange(
        [
            new Usuario
            {
                Id = Guid.Parse("C673BA2A-7F3C-4F76-B9FD-DA3121EFEF2C"),
                NombreUsuario = "admin",
                Contrasena = _passwordService.Hashear("1234"),
                Rol = "Ejecutivo"
            },
            new Usuario
            {
                Id = Guid.Parse("2BE48BC4-A874-44A4-B5C5-CC94124558A9"),
                NombreUsuario = "empleado",
                Contrasena = _passwordService.Hashear("1234"),
                Rol = "Empleado"
            }
        ]);
    }
}
