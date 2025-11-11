using Aplicacion.Interfaces.Repositorios;
using Aplicacion.Interfaces.Servicios;
using Aplicacion.Servicios;
using Infraestructura.Datos;
using Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));
builder.Services.AddScoped<IProveedorRepositorio, ProveedorRepositorio>();
builder.Services.AddScoped<IProveedorServicio, ProveedorServicio>();
builder.Services.AddScoped<ITecnicoRepositorio, TecnicoRepositorio>();
builder.Services.AddScoped<ITecnicoServicio, TecnicoServicio>();
builder.Services.AddScoped<IEquipoRepositorio, EquipoRepositorio>();
builder.Services.AddScoped<IEquipoServicio, EquipoServicio>();
builder.Services.AddScoped<ITipoEquipoRepositorio, TipoEquipoRepositorio>();
builder.Services.AddScoped<ITipoEquipoServicio, TipoEquipoServicio>();
builder.Services.AddScoped<IAdquisicionRepositorio, AdquisicionRepositorio>();
builder.Services.AddScoped<IAdquisicionServicio, AdquisicionServicio>();
builder.Services.AddScoped<IMantenimientoRepositorio, MantenimientoRepositorio>();
builder.Services.AddScoped<IMantenimientoServicio, MantenimientoServicio>();
builder.Services.AddScoped<ITipoMantenimientoRepositorio, TipoMantenimientoRepositorio>();
builder.Services.AddScoped<ITipoMantenimientoServicio, TipoMantenimientoServicio>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
