using Client.Application.Abstractions;

namespace Client.Application.Clientes.Commands.CreateCliente;

public class CreateClienteHandler(
    IApplicationDbContext dbContext,
    IPasswordHasher passwordHasher)
    : ICommandHandler<CreateClienteCommand, CreateClienteResult>
{
    public async Task<CreateClienteResult> Handle(CreateClienteCommand command, CancellationToken cancellationToken)
    {
        var cliente = CreateNewCliente(command.Cliente);

        dbContext.Clientes.Add(cliente);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateClienteResult(cliente.Id.Value);
    }

    private Cliente CreateNewCliente(ClienteDto dto)
    {
        var passwordHash = passwordHasher.Hash(dto.Password);

        return Cliente.Crear(
            nombre: dto.Nombre,
            genero: dto.Genero,
            edad: dto.Edad,
            identificacion: dto.Identificacion,
            direccion: dto.Direccion,
            telefono: dto.Telefono,
            password: passwordHash);
    }
}