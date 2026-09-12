using BuildingBlocks.Auth;
using Client.Application.Abstractions;

namespace Client.Application.Clientes.Queries.Login;

public class LoginHandler(
    IApplicationDbContext dbContext,
    IPasswordHasher passwordHasher,
    ILoginTokenGenerator tokenGenerator)
    : IQueryHandler<LoginQuery, LoginResult>
{
    public async Task<LoginResult> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var cliente = await dbContext.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Identificacion == query.Login.Identificacion, cancellationToken);

        if (cliente is null || !passwordHasher.Verify(query.Login.Password, cliente.Password))
            throw new UnauthorizedAccessException("Identificacion o password incorrectos");

        if (cliente.Estado == EstadoCliente.Inactivo)
            throw new UnauthorizedAccessException("El cliente se encuentra inactivo");

        var accessToken = tokenGenerator.GenerateAccessToken(cliente.Id.Value);

        return new LoginResult(
            accessToken.Value,
            accessToken.ExpiresIn);
    }
}