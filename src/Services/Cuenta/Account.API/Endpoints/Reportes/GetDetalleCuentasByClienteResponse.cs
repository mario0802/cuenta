using Account.Application.Common.Models;
using Account.Application.Reportes.GetDetalleCuentasByCliente;

namespace Account.API.Endpoints.Reportes;

public record GetDetalleCuentasByClienteResponse(
    ClienteDto Cliente,
    List<CuentaMovimientosDto> Cuentas);

public class GetDetalleCuentasByCliente : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/reportes/{clienteId:guid}", async (
            Guid clienteId,
            DateTime? fechaDesde,
            DateTime? fechaHasta,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetDetalleCuentasByClienteQuery(clienteId, fechaDesde, fechaHasta);

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetDetalleCuentasByClienteResponse>();
            return Results.Ok(response);
        })
        .WithName("GetDetalleCuentasByCliente")
        .Produces<GetDetalleCuentasByClienteResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Detalle Cuentas By Cliente")
        .WithDescription("Obtiene el cliente, sus cuentas, y todos sus movimientos filtrados por rango de fechas, sin paginación");
    }
}