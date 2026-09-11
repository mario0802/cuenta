
namespace Client.Application.Clientes.Queries.GetClientes;

public record GetClientesQuery(PaginationRequest PaginationRequest)
    : IQuery<GetClientesResult>;

public record GetClientesResult(PaginatedResult<ClienteDetailDto> Clientes);