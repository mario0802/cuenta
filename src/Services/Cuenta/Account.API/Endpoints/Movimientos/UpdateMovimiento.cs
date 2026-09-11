using Account.Application.Movimientos.Commands.UpdateMovimiento;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Endpoints.Movimientos;

public record UpdateMovimientoRequest(TipoMovimiento TipoMovimiento, decimal Valor, DateTime? Fecha);
public record UpdateMovimientoResponse(Guid Id, decimal SaldoActual);

public class UpdateMovimiento : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/movimientos/{movimientoId:guid}/cuentas/{cuentaId:guid}", async (
            Guid movimientoId,
            Guid cuentaId,
            [FromBody] UpdateMovimientoRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateMovimientoCommand(
                cuentaId, movimientoId, request.TipoMovimiento, request.Valor, request.Fecha);

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<UpdateMovimientoResponse>();
            return Results.Ok(response);
        })
        .WithName("UpdateMovimiento")
        .Produces<UpdateMovimientoResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Movimiento")
        .WithDescription("Corrige un movimiento existente y recalcula los saldos de la cuenta");
    }
}