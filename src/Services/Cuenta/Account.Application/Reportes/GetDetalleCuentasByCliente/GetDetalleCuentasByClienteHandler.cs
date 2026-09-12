using Account.Application.Common.Interfaces;

namespace Account.Application.Reportes.GetDetalleCuentasByCliente;

public class GetDetalleCuentasByClienteHandler(
    IApplicationDbContext dbContext,
    IClientServiceClient clienteServiceClient)
    : IQueryHandler<GetDetalleCuentasByClienteQuery, GetDetalleCuentasByClienteResult>
{
    public async Task<GetDetalleCuentasByClienteResult> Handle(
        GetDetalleCuentasByClienteQuery query, CancellationToken cancellationToken)
    {
        var clienteResponse = await clienteServiceClient.GetClienteByIdAsync(
            query.ClienteId, cancellationToken);

        var cuentas = await dbContext.Cuentas
            .Where(c => c.ClienteId == ClienteId.Of(query.ClienteId))
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        if (cuentas.Count == 0)
        {
            return new GetDetalleCuentasByClienteResult(clienteResponse.Cliente, []);
        }

        var cuentaIds = cuentas.Select(c => c.Id).ToList();

        var movimientosQuery = dbContext.Movimientos
            .Where(m => cuentaIds.Contains(m.CuentaId))
            .AsNoTracking();

        if (query.FechaDesde.HasValue)
        {
            movimientosQuery = movimientosQuery.Where(m => m.Fecha >= query.FechaDesde.Value);
        }

        if (query.FechaHasta.HasValue)
        {
            var fechaHastaInclusive = query.FechaHasta.Value.Date.AddDays(1).AddTicks(-1);
            movimientosQuery = movimientosQuery.Where(m => m.Fecha <= fechaHastaInclusive);
        }

        var movimientos = await movimientosQuery
            .OrderByDescending(m => m.Fecha)
            .ToListAsync(cancellationToken);

        // 4. Agrupar movimientos por cuenta
        var movimientosPorCuenta = movimientos
            .GroupBy(m => m.CuentaId)
            .ToDictionary(g => g.Key, g => g.ToList());

        var cuentasConMovimientos = cuentas
            .Select(c => new CuentaMovimientosDto(
                c.Id.Value,
                c.NumeroCuenta,
                c.SaldoInicial,
                movimientosPorCuenta.TryGetValue(c.Id, out var movs)
                    ? movs.Select(m => new MovimientoDto(
                        m.Id.Value, m.TipoMovimiento, m.Fecha, m.Valor, m.Saldo)).ToList()
                    : []))
            .ToList();

        return new GetDetalleCuentasByClienteResult(clienteResponse.Cliente, cuentasConMovimientos);
    }
}
