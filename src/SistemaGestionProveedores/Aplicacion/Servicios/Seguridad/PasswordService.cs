using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Servicios.Seguridad;

public class PasswordService
{
    private readonly PasswordHasher<string> _passwordHasher = new();

    public string Hashear(string password)
    {
        return _passwordHasher.HashPassword(null!, password);
    }

    public bool Verificar(string passwordPlano, string hashGuardado)
    {
        var result = _passwordHasher.VerifyHashedPassword(null!, hashGuardado, passwordPlano);
        return result == PasswordVerificationResult.Success;
    }
}
