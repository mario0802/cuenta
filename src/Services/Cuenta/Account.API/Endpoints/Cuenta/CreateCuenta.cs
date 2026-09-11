using Account.Application.Cuentas.Commands.CreateCuenta;
using Microsoft.AspNetCore.Mvc;
namespace Account.API.Endpoints;
public record CreateCuentaRequest(CuentaDto Cuenta);
public record CreateCuentaResponse(Guid Id);

public class CreateCuenta : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/cuentas", async (
            [FromBody] CreateCuentaRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<CreateCuentaCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<CreateCuentaResponse>();
            return Results.Created($"/cuentas/{response.Id}", response);
        })
        .WithName("CreateCuenta")
        .Produces<CreateCuentaResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Cuenta")
        .WithDescription("Registra una nueva cuenta en el sistema");
    }
}