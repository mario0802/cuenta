namespace Client.Application.Clientes.Queries.GetClienteById;

public record GetClienteByIdQuery(Guid Id)
    : IQuery<GetClienteByIdResult>;

public record GetClienteByIdResult(ClienteDetailDto Cliente);