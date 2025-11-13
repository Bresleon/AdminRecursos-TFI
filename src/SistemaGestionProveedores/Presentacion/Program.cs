using Aplicacion.Interfaces.Repositorios;
using Aplicacion.Interfaces.Servicios;
using Aplicacion.Servicios;
using Infraestructura.Datos;
using Infraestructura.Repositorios;
using Microsoft.AspNetCore.Authentication.Cookies;
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

// Configuración para autenticación
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

var app = builder.Build();

// Insertar usuarios al iniciar la app
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
    DataSeeder.Seed(context);
}

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
