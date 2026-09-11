using Client.Application.Clientes.Queries.GetClientes;
namespace Client.API.Endpoints;
public record GetClientesResponse(PaginatedResult<ClienteDetailDto> Clientes);

public class GetClientes : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/clientes", async (
            [AsParameters] PaginationRequest paginationRequest,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetClientesQuery(paginationRequest);

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetClientesResponse>();
            return Results.Ok(response);
        })
        .WithName("GetClientes")
        .Produces<GetClientesResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Clientes")
        .WithDescription("Obtiene la lista paginada de clientes");
    }
}