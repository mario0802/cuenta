namespace Client.Application.Clientes.Queries.GetClientes;

public class GetClientesHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetClientesQuery, GetClientesResult>
{
    public async Task<GetClientesResult> Handle(GetClientesQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Clientes
            .LongCountAsync(cancellationToken);

        var clientes = await dbContext.Clientes
            .AsNoTracking()
            .OrderBy(c => c.Nombre)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .Select(c => new ClienteDetailDto(
                c.Id.Value,
                c.Nombre,
                c.Genero,
                c.Edad,
                c.Identificacion,
                c.Direccion,
                c.Telefono,
                c.Estado))
            .ToListAsync(cancellationToken);

        return new GetClientesResult(
            new PaginatedResult<ClienteDetailDto>(pageIndex, pageSize, totalCount, clientes));
    }
}
