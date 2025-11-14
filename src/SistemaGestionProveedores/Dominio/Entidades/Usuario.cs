namespace Dominio.Entidades;

public class Usuario : EntidadBase
{
    public Guid Id { get; set; }
    public string NombreUsuario { get; set; }
    public string Contrasena { get; set; }
    public string Rol { get; set; }
}

