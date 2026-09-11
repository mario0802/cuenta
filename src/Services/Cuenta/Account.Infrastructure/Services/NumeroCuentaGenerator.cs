using Account.Application.Abstractions;
using System.Security.Cryptography;

namespace Account.Infrastructure.Services;

public class NumeroCuentaGenerator : INumeroCuentaGenerator
{
    public string Generar()
    {
        var milisegundos = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        return milisegundos.ToString();
    }
}