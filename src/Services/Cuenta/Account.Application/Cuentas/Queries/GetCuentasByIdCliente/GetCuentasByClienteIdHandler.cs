
namespace Account.Application.Cuentas.Queries.GetCuentasByIdCliente;

public class GetCuentasByClienteIdHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetCuentasByClienteIdQuery, GetCuentasByClienteIdResult>
{
    public async Task<GetCuentasByClienteIdResult> Handle(
        GetCuentasByClienteIdQuery query, CancellationToken cancellationToken)
    {
        var clienteId = ClienteId.Of(query.ClienteId);

        var cuentas = await dbContext.Cuentas
            .AsNoTracking()
            .Where(c => c.ClienteId == clienteId)
            .Select(c => new CuentaResumenDto(
                c.Id.Value,
                c.NumeroCuenta,
                c.TipoCuenta,
                c.SaldoInicial,
                c.Estado))
            .ToListAsync(cancellationToken);

        return new GetCuentasByClienteIdResult(cuentas);
    }
}