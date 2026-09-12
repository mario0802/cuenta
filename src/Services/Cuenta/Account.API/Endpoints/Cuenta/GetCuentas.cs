using Account.Application.Cuentas.Queries.GetCuentas;
using BuildingBlocks.Auth;
using System.Security.Claims;

namespace Account.API.Endpoints.Cuenta;
public record GetCuentasResponse(PaginatedResult<CuentaResumenDto> Cuentas);

public class GetCuentas : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/cuentas", async (
            //ClaimsPrincipal client,
            [AsParameters] PaginationRequest paginationRequest,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            //var clienteId = client.GetClienteId();
            var query = new GetCuentasQuery(paginationRequest);

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetCuentasResponse>();
            return Results.Ok(response);
        })
        //.RequireAuthorization()
        .WithName("GetCuentas")
        .Produces<GetCuentasResponse>(StatusCodes.Status200OK)
        .WithSummary("Get Cuentas")
        .WithDescription("Obtiene el listado paginado de cuentas");
    }
}