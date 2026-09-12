using System.Security.Claims;
namespace BuildingBlocks.Auth;

public static class ClaimsPrincipalExtensions
{
    private const string ClienteIdClaimType = "clienteId";

    public static Guid GetClienteId(this ClaimsPrincipal cliente)
    {
        var value = cliente.FindFirst(ClienteIdClaimType)?.Value
            ?? throw new UnauthorizedAccessException("Token does not contain a clienteId claim.");

        if (!Guid.TryParse(value, out var clienteId))
            throw new UnauthorizedAccessException("clienteId claim is not a valid GUID.");

        return clienteId;
    }
}