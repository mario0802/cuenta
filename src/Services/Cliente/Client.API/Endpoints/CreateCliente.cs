using Client.Application.Clientes.Commands.CreateCliente;

namespace Client.API.Endpoints;
public record CreateClienteRequest(ClienteDto Cliente);
public record CreateClienteResponse(Guid Id);

public class CreateCliente : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/clientes", async (
            [FromBody] CreateClienteRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<CreateClienteCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<CreateClienteResponse>();
            return Results.Created($"/clientes/{response.Id}", response);
        })
        .WithName("CreateCliente")
        .Produces<CreateClienteResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Cliente")
        .WithDescription("Registra un nuevo cliente en el sistema");
    }
}