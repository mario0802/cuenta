namespace Client.Application.Clientes.Queries.GetClienteById;

public class GetClienteByIdHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetClienteByIdQuery, GetClienteByIdResult>
{
    public async Task<GetClienteByIdResult> Handle(GetClienteByIdQuery query, CancellationToken cancellationToken)
    {
        var clienteId = ClienteId.Of(query.Id);

        var cliente = await dbContext.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == clienteId, cancellationToken);

        if (cliente is null)
            throw new NotFoundException(nameof(Cliente), query.Id);

        var clienteDto = new ClienteDetailDto(
            cliente.Id.Value,
            cliente.Nombre,
            cliente.Genero,
            cliente.Edad,
            cliente.Identificacion,
            cliente.Direccion,
            cliente.Telefono,
            cliente.Estado);

        return new GetClienteByIdResult(clienteDto);
    }
}