using Client.Application.Clientes.Queries.Login;
using Microsoft.AspNetCore.Identity.Data;

namespace Client.API.Endpoints;

public record LoginRequest(LoginDto Login);
public record LoginResponse(
    string AccessToken,
    TimeSpan ExpiresIn);

public class Login : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/clientes/login", async (
            [FromBody] LoginRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new LoginQuery(request.Login);

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<LoginResponse>();
            return Results.Ok(response);
        })
        .WithName("Login")
        .Produces<LoginResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .WithSummary("Login")
        .WithDescription("Autentica un cliente y devuelve access token + refresh token");
    }
}