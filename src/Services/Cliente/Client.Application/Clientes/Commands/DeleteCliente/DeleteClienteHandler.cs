
namespace Client.Application.Clientes.Commands.DeleteCliente;

public class DeleteClienteHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeleteClienteCommand, DeleteClienteResult>
{
    public async Task<DeleteClienteResult> Handle(DeleteClienteCommand command, CancellationToken cancellationToken)
    {
        var clienteId = ClienteId.Of(command.Id);

        var cliente = await dbContext.Clientes
            .FindAsync([clienteId], cancellationToken);

        if (cliente is null)
            throw new NotFoundException(nameof(Cliente), command.Id);

        cliente.Inactivar();

        dbContext.Clientes.Update(cliente);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteClienteResult(true);
    }
}