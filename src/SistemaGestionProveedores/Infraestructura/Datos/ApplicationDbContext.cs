using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Datos;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Proveedor> Proveedores { get; set; }
    public DbSet<Equipo> Equipos { get; set; }
    public DbSet<TipoEquipo> TiposEquipo { get; set; }
    public DbSet<Tecnico> Tecnicos { get; set; }
    public DbSet<Adquisicion> Adquisiciones { get; set; }
    public DbSet<TipoMantenimiento> TiposMantenimiento { get; set; }
    public DbSet<Mantenimiento> Mantenimientos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ProveedoresConfig(modelBuilder);
        EquiposConfig(modelBuilder);
        TiposEquipoConfig(modelBuilder);
        TecnicosConfig(modelBuilder);
        AdquisicionesConfig(modelBuilder);
        TiposMantenimientoConfig(modelBuilder);
        MantenimientosConfig(modelBuilder);
        UsuariosConfig(modelBuilder);
    }

    private void ProveedoresConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.ToTable("proveedores");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.RazonSocial).IsRequired().HasMaxLength(100);
            entity.Property(p => p.CUIT).IsRequired().HasMaxLength(20).IsFixedLength();
            entity.Property(p => p.Email).HasMaxLength(100);
            entity.Property(p => p.Direccion).HasMaxLength(100);
            entity.Property(p => p.Telefono).HasMaxLength(15);
            entity.Property(p => p.Calificacion).HasColumnType("float").HasDefaultValue(0);
            entity.Property(p => p.Activado).HasDefaultValue(1);

            entity.HasData(Iniciales.Instance.Proveedores);
        });
    }

    private void EquiposConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.ToTable("equipos");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            entity.HasOne(e => e.TipoEquipo)
                  .WithMany(te => te.Equipos)
                  .HasForeignKey(e => e.TipoEquipoId);
            entity.HasOne(e => e.Proveedor)
                  .WithMany(p => p.Equipos)
                  .HasForeignKey(e => e.ProveedorId);
            entity.Property(e => e.Activado).HasDefaultValue(1);

            entity.HasData(Iniciales.Instance.Equipos);
        });
    }

    private void TiposEquipoConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TipoEquipo>(entity =>
        {
            entity.ToTable("tipos_equipos");
            entity.HasKey(te => te.Id);
            entity.Property(te => te.Nombre).IsRequired().HasMaxLength(100);

            entity.HasData(Iniciales.Instance.TiposEquipo);
        });
    }

    private void TecnicosConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tecnico>(entity =>
        {
            entity.ToTable("tecnicos");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Nombre).IsRequired().HasMaxLength(50);
            entity.Property(t => t.Apellido).IsRequired().HasMaxLength(50);
            entity.Property(t => t.DNI).IsRequired().HasMaxLength(8).IsFixedLength();
            entity.Property(t => t.Telefono).IsRequired().HasMaxLength(15).IsFixedLength();
            entity.Property(t => t.Calificacion).HasColumnType("float").HasDefaultValue(0);
            entity.Property(p => p.Activado).HasDefaultValue(1);

            entity.HasData(Iniciales.Instance.Tecnicos);
        });
    }

    private void AdquisicionesConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adquisicion>(entity =>
        {
            entity.ToTable("adquisiciones");
            entity.HasKey(a => a.Id);
            entity.Property(a => a.NumeroSerie).IsRequired().HasMaxLength(50);
            entity.Property(a => a.FechaAdquisicion).IsRequired();
            entity.Property(a => a.FechaFinGarantia).IsRequired();
            entity.Property(a => a.Costo).IsRequired().HasColumnType("decimal(18,2)");
            entity.HasOne(a => a.Equipo)
                  .WithMany(e => e.Adquisiciones)
                  .HasForeignKey(a => a.EquipoId)
                  .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Tecnico)
                  .WithMany(t => t.Adquisiciones)
                  .HasForeignKey(e => e.TecnicoId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(Iniciales.Instance.Adquisiciones);
        });
    }

    private void TiposMantenimientoConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TipoMantenimiento>(entity =>
        {
            entity.ToTable("tipos_mantenimiento");
            entity.HasKey(tm => tm.Id);
            entity.Property(tm => tm.Descripcion).IsRequired().HasMaxLength(100);

            entity.HasData(Iniciales.Instance.TiposMantenimiento);
        });
    }

    private void MantenimientosConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mantenimiento>(entity =>
        {
            entity.ToTable("mantenimientos");
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Estado).HasConversion<string>().IsRequired();
            entity.Property(m => m.Fecha).IsRequired();
            entity.Property(m => m.Descripcion).IsRequired().HasMaxLength(250);
            entity.Property(m => m.Costo).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(m => m.Calificacion).HasColumnType("float").HasDefaultValue(0);
            entity.HasOne(m => m.Adquisicion)
                  .WithMany(a => a.Mantenimientos)
                  .HasForeignKey(m => m.AdquisicionId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(m => m.Tecnico)
                  .WithMany(t => t.Mantenimientos)
                  .HasForeignKey(m => m.TecnicoId)
                  .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(m => m.TipoMantenimiento)
                  .WithMany(tm => tm.Mantenimientos)
                  .HasForeignKey(m => m.TipoMantenimientoId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(Iniciales.Instance.Mantenimientos);
        });
    }

    private void UsuariosConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("usuarios");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.NombreUsuario).IsRequired();
            entity.Property(u => u.Contrasena).IsRequired();
            entity.Property(u => u.Rol).IsRequired();
        });
    }
}
