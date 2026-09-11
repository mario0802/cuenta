using Client.Application.Clientes.Commands.DeleteCliente;

namespace Client.API.Endpoints;

public record DeleteClienteResponse(bool IsSuccess);
public class DeleteCliente : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/clientes/{id}", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteClienteCommand(id);

            var result = await sender.Send(command, cancellationToken);

            var response = new DeleteClienteResponse(result.IsSuccess);
            return Results.Ok(response);
        })
        .WithName("DeleteCliente")
        .Produces<DeleteClienteResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Cliente")
        .WithDescription("Inactiva (soft-delete) un cliente existente");
    }
}