using Account.Application.Movimientos.Commands.CreateMovimiento;
using Account.Application.Movimientos.Commands.UpdateMovimiento;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Endpoints.Movimientos;

public record CreateMovimientoRequest(MovimientoDto Movimiento);
public record CreateMovimientoResponse(Guid Id, decimal SaldoResultante);

public class CreateMovimiento : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/movimientos", async (
            [FromBody] CreateMovimientoRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<CreateMovimientoCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<CreateMovimientoResponse>();
            return Results.Created($"/movimientos/{response.Id}", response);
        })
        .WithName("CreateMovimiento")
        .Produces<CreateMovimientoResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Create Movimiento")
        .WithDescription("Registra un movimiento (depósito/retiro) sobre una cuenta existente");
    }
}