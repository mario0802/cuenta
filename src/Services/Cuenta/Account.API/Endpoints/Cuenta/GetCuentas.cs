using Account.Application.Cuentas.Queries.GetCuentas;

namespace Account.API.Endpoints.Cuenta;
public record GetCuentasResponse(PaginatedResult<CuentaResumenDto> Cuentas);

public class GetCuentas : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/cuentas", async (
            [AsParameters] PaginationRequest paginationRequest,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetCuentasQuery(paginationRequest);

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetCuentasResponse>();
            return Results.Ok(response);
        })
        .WithName("GetCuentas")
        .Produces<GetCuentasResponse>(StatusCodes.Status200OK)
        .WithSummary("Get Cuentas")
        .WithDescription("Obtiene el listado paginado de cuentas");
    }
}