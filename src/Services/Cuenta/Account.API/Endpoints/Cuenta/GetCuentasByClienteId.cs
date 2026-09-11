using Account.Application.Cuentas.Queries.GetCuentasByIdCliente;

namespace Account.API.Endpoints.Cuenta;

public record GetCuentasByClienteIdResponse(List<CuentaResumenDto> Cuentas);

public class GetCuentasByClienteId : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/clientes/{clienteId:guid}/cuentas", async (
            Guid clienteId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetCuentasByClienteIdQuery(clienteId);

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetCuentasByClienteIdResponse>();
            return Results.Ok(response);
        })
        .WithName("GetCuentasByClienteId")
        .Produces<GetCuentasByClienteIdResponse>(StatusCodes.Status200OK)
        .WithSummary("Get Cuentas By ClienteId")
        .WithDescription("Obtiene todas las cuentas asociadas a un cliente");
    }
}