namespace Account.Application.Movimientos.Queries.GetMovimientosByCuentaAndFecha;

public class GetMovimientosByCuentaAndFechaHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetMovimientosByCuentaAndFechaQuery, GetMovimientosByCuentaAndFechaResult>
{
    public async Task<GetMovimientosByCuentaAndFechaResult> Handle(
        GetMovimientosByCuentaAndFechaQuery query, CancellationToken cancellationToken)
    {
        var cuentaId = CuentaId.Of(query.CuentaId);

        var cuentaExiste = await dbContext.Cuentas
            .AnyAsync(c => c.Id == cuentaId, cancellationToken);

        if (!cuentaExiste)
        {
            throw new NotFoundException($"No se ha encontrado la cuenta {query.CuentaId}");
        }

        var baseQuery = dbContext.Movimientos
            .Where(m => m.CuentaId == cuentaId)
            .AsNoTracking();

        if (query.FechaDesde.HasValue)
        {
            baseQuery = baseQuery.Where(m => m.Fecha >= query.FechaDesde.Value);
        }

        if (query.FechaHasta.HasValue)
        {
            // Incluye todo el día de FechaHasta si viene sin componente de hora
            var fechaHastaInclusive = query.FechaHasta.Value.Date.AddDays(1).AddTicks(-1);
            baseQuery = baseQuery.Where(m => m.Fecha <= fechaHastaInclusive);
        }

        baseQuery = baseQuery.OrderByDescending(m => m.Fecha);

        var totalCount = await baseQuery.LongCountAsync(cancellationToken);

        var movimientos = await baseQuery
            .Select(m => new MovimientoDto(
                m.Id.Value,
                m.CuentaId.Value,
                m.TipoMovimiento,
                m.Fecha,
                m.Valor,
                m.Saldo))
            .ToListAsync(cancellationToken);

        return new GetMovimientosByCuentaAndFechaResult(movimientos);
    }
}
