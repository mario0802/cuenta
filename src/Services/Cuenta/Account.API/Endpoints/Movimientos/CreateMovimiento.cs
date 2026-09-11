using Account.Application.Movimientos.Commands.CreateMovimiento;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Endpoints.Movimientos;

public record CreateMovimientoRequest(MovimientoDto Movimiento);
public record CreateMovimientoResponse(Guid Id, decimal SaldoResultante);

public class CreateMovimiento : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/movimientos", async (
            Guid cuentaId,
            [FromBody] CreateMovimientoRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var movimientoDto = request.Movimiento with { CuentaId = cuentaId };
            var command = new CreateMovimientoCommand(movimientoDto);

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<CreateMovimientoResponse>();
            return Results.Created($"/movimientos/{cuentaId}/movimientos/{response.Id}", response);
        })
        .WithName("CreateMovimiento")
        .Produces<CreateMovimientoResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Create Movimiento")
        .WithDescription("Registra un movimiento (depósito/retiro) sobre una cuenta existente");
    }
}