namespace Infraestructura.Datos;

public static class DataSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        if (!context.Usuarios.Any())
        {
            context.Usuarios.AddRange(Iniciales.Instance.Usuarios);
            context.SaveChanges();
        }
    }
}
