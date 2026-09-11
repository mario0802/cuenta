namespace Client.Infrastructure.Security;

public class BCryptPasswordHasher : IPasswordHasher
{
    // WorkFactor: a mayor valor, más lento y más seguro. 12 es un buen default en 2026.
    private const int WorkFactor = 12;

    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, workFactor: WorkFactor);
    }

    public bool Verify(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}