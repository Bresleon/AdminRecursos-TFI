using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Datos;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Proveedor> Proveedores { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<TipoProducto> TiposProducto { get; set; }
    public DbSet<Tecnico> Tecnicos { get; set; }
    public DbSet<Visita> Visitas { get; set; }
    public DbSet<ProductoEnVisita> ProductosEnVisita { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ProveedoresConfig(modelBuilder);
        ProductosConfig(modelBuilder);
        TiposProductoConfig(modelBuilder);
        TecnicosConfig(modelBuilder);
        VisitasConfig(modelBuilder);
        ProductosEnVisitaConfig(modelBuilder);
    }

    private void ProveedoresConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.ToTable("proveedores");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.RazonSocial).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CUIT).IsRequired().HasMaxLength(20).IsFixedLength();
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(15);
            entity.Property(e => e.Calificacion).HasColumnType("float").HasDefaultValue(0);
        });
    }

    private void ProductosConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.ToTable("productos");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            entity.HasOne(e => e.TipoProducto)
                  .WithMany(tp => tp.Productos)
                  .HasForeignKey(e => e.TipoProductoId);
            entity.HasOne(e => e.Proveedor)
                  .WithMany(p => p.Productos)
                  .HasForeignKey(e => e.ProveedorId);
        });
    }

    private void TiposProductoConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TipoProducto>(entity =>
        {
            entity.ToTable("tipos_productos");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
        });
    }

    private void TecnicosConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tecnico>(entity =>
        {
            entity.ToTable("tecnicos");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Apellido).IsRequired().HasMaxLength(50);
            entity.Property(e => e.DNI).IsRequired().HasMaxLength(8).IsFixedLength();
            entity.Property(e => e.Telefono).IsRequired().HasMaxLength(15).IsFixedLength();
            entity.Property(e => e.Calificacion).HasColumnType("float").HasDefaultValue(0);
        });
    }

    private void VisitasConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Visita>(entity =>
        {
            entity.ToTable("visitas");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FechaHora).IsRequired();
            entity.Property(e => e.MontoTotal).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(e => e.Calificacion).HasColumnType("float").HasDefaultValue(0);
            entity.Property(e => e.Estado).HasConversion<string>().IsRequired();
            entity.Property(e => e.Observaciones).HasMaxLength(1000);
            entity.HasOne(e => e.Tecnico)
                  .WithMany(t => t.Visitas)
                  .HasForeignKey(e => e.TecnicoId);
        });
    }

    private void ProductosEnVisitaConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductoEnVisita>(entity =>
        {
            entity.ToTable("productos_visitas");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.NumeroSerie).IsRequired().HasMaxLength(50);
            entity.Property(e => e.PrecioUnitario).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(e => e.FinGarantia).IsRequired();
            entity.Property(e => e.Concepto).HasConversion<string>().IsRequired();
            entity.Property(e => e.Observaciones).HasMaxLength(1000);
            entity.HasOne(e => e.Visita)
                  .WithMany(v => v.ProductosEnVisita)
                  .HasForeignKey(e => e.VisitaId)
                  .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Producto)
                  .WithMany(p => p.ProductosEnVisita)
                  .HasForeignKey(e => e.ProductoId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
