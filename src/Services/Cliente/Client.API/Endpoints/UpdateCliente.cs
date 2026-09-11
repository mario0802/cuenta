using Client.Application.Clientes.Commands.UpdateCliente;

namespace Client.API.Endpoints;
public record UpdateClienteRequest(UpdateClienteDto Cliente);
public record UpdateClienteResponse(bool IsSuccess);

public class UpdateCliente : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/clientes/{id}", async (
            Guid id,
            [FromBody] UpdateClienteRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            if (id != request.Cliente.Id)
                return Results.BadRequest("El Id de la ruta no coincide con el Id del body");

            var command = request.Adapt<UpdateClienteCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<UpdateClienteResponse>();
            return Results.Ok(response);
        })
        .WithName("UpdateCliente")
        .Produces<UpdateClienteResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Cliente")
        .WithDescription("Actualiza los datos básicos de un cliente existente");
    }
}