using Client.Application.Clientes.Queries.GetClienteById;

namespace Client.API.Endpoints;

public record GetClienteByIdResponse(
    Guid Id,
    string Nombre,
    Genero Genero,
    int Edad,
    string Identificacion,
    string Direccion,
    string Telefono,
    EstadoCliente Estado);

public class GetClienteById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/clientes/{id}", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetClienteByIdQuery(id);

            var result = await sender.Send(query, cancellationToken);

            var response = result.Cliente.Adapt<GetClienteByIdResponse>();
            return Results.Ok(response);
        })
        .WithName("GetClienteById")
        .Produces<GetClienteByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Cliente By Id")
        .WithDescription("Obtiene los datos de un cliente por su Id");
    }
}