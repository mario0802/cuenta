
namespace Account.Application.Cuentas.Queries.GetCuentas;

public class GetCuentasHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetCuentasQuery, GetCuentasResult>
{
    public async Task<GetCuentasResult> Handle(GetCuentasQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Cuentas
            .LongCountAsync(cancellationToken);

        var cuentas = await dbContext.Cuentas
            .AsNoTracking()
            .OrderBy(c => c.NumeroCuenta)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .Select(c => new CuentaResumenDto(
                c.Id.Value,
                c.ClienteId.Value,
                c.NumeroCuenta,
                c.TipoCuenta,
                c.SaldoInicial,
                c.Estado))
            .ToListAsync(cancellationToken);

        var resultado = new PaginatedResult<CuentaResumenDto>(
            pageIndex, pageSize, totalCount, cuentas);

        return new GetCuentasResult(resultado);
    }
}