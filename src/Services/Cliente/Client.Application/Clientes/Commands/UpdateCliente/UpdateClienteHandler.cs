
namespace Client.Application.Clientes.Commands.UpdateCliente;

public class UpdateClienteHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateClienteCommand, UpdateClienteResult>
{
    public async Task<UpdateClienteResult> Handle(UpdateClienteCommand command, CancellationToken cancellationToken)
    {
        var clienteId = ClienteId.Of(command.Cliente.Id);

        var cliente = await dbContext.Clientes
            .FindAsync([clienteId], cancellationToken);

        if (cliente is null)
            throw new NotFoundException(nameof(Cliente), command.Cliente.Id);

        UpdateClienteWithNewValues(cliente, command.Cliente);

        dbContext.Clientes.Update(cliente);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateClienteResult(true);
    }

    private void UpdateClienteWithNewValues(Cliente cliente, UpdateClienteDto dto)
    {
        cliente.ActualizarDatos(
            nombre: dto.Nombre,
            genero: dto.Genero,
            edad: dto.Edad,
            direccion: dto.Direccion,
            telefono: dto.Telefono);
    }
}