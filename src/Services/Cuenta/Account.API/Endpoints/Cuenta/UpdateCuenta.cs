using Account.Application.Cuentas.Commands.UpdateCuenta;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Endpoints;

public record UpdateCuentaRequest(UpdateCuentaDto Cuenta);
public record UpdateCuentaResponse(Guid Id);

public class UpdateCuenta : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/cuentas/{id:guid}", async (
            Guid id,
            [FromBody] UpdateCuentaRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateCuentaCommand(id, request.Cuenta);

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<UpdateCuentaResponse>();
            return Results.Ok(response);
        })
        .WithName("UpdateCuenta")
        .Produces<UpdateCuentaResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Cuenta")
        .WithDescription("Actualiza el tipo y estado de una cuenta existente");
    }
}