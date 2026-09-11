using Account.Application.Cuentas.Queries.GetCuentaById;
using Account.Domain.Enums;

namespace Account.API.Endpoints.Cuenta;

public record GetCuentaByIdResponse(
    Guid Id,
    Guid ClienteId,
    string NumeroCuenta,
    TipoCuenta TipoCuenta,
    decimal SaldoInicial,
    EstadoCuenta Estado);

public class GetCuentaById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/cuentas/{id:guid}", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetCuentaByIdQuery(id);

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetCuentaByIdResponse>();
            return Results.Ok(response);
        })
        .WithName("GetCuentaById")
        .Produces<GetCuentaByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Cuenta By Id")
        .WithDescription("Obtiene el detalle de una cuenta por su Id");
    }
}