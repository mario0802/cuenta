using Account.Application.Movimientos.Queries.GetMovimientosByCuentaAndFecha;

namespace Account.API.Endpoints.Movimientos;

public record GetMovimientosByCuentaAndFechaResponse(List<MovimientoDto> Movimientos);

public class GetMovimientosByCuentaAndFecha : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/movimientos/cuentas/{cuentaId:guid}/reporte", async (
            Guid cuentaId,
            [AsParameters] PaginationRequest pagination,
            DateTime? fechaDesde,
            DateTime? fechaHasta,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetMovimientosByCuentaAndFechaQuery(
                cuentaId, fechaDesde, fechaHasta);

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetMovimientosByCuentaAndFechaResponse>();
            return Results.Ok(response);
        })
        .WithName("GetMovimientosByCuentaAndFecha")
        .Produces<GetMovimientosByCuentaAndFechaResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Movimientos By Cuenta And Fecha")
        .WithDescription("Obtiene el historial paginado de movimientos de una cuenta filtrado por rango de fechas");
    }
}