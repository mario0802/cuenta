namespace Account.Application.Cuentas.Queries.GetCuentaById;

public class GetCuentaByIdHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetCuentaByIdQuery, GetCuentaByIdResult>
{
    public async Task<GetCuentaByIdResult> Handle(GetCuentaByIdQuery query, CancellationToken cancellationToken)
    {
        var cuentaId = CuentaId.Of(query.Id);

        var cuenta = await dbContext.Cuentas
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == cuentaId, cancellationToken);

        if (cuenta is null)
        {
            throw new NotFoundException(nameof(Cuenta), query.Id);
        }

        return new GetCuentaByIdResult(
            cuenta.Id.Value,
            cuenta.ClienteId.Value,
            cuenta.NumeroCuenta,
            cuenta.TipoCuenta,
            cuenta.SaldoInicial,
            cuenta.Estado);
    }
}