using Account.Application.Movimientos.Queries.GetMovimientosByCuenta;

namespace Account.API.Endpoints.Movimientos;

public record GetMovimientosByCuentaResponse(PaginatedResult<MovimientoDto> Movimientos);

public class GetMovimientosByCuenta : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/movimientos/{cuentaId:guid}", async (
            Guid cuentaId,
            [AsParameters] PaginationRequest pagination,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetMovimientosByCuentaQuery(
                cuentaId, pagination.PageIndex, pagination.PageSize);

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetMovimientosByCuentaResponse>();
            return Results.Ok(response);
        })
        .WithName("GetMovimientosByCuenta")
        .Produces<GetMovimientosByCuentaResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Movimientos By Cuenta")
        .WithDescription("Obtiene el historial paginado de movimientos de una cuenta");
    }
}