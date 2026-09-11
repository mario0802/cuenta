using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Movimientos.Queries.GetMovimientosByCuenta;

public class GetMovimientosByCuentaHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetMovimientosByCuentaQuery, GetMovimientosByCuentaResult>
{
    public async Task<GetMovimientosByCuentaResult> Handle(
        GetMovimientosByCuentaQuery query, CancellationToken cancellationToken)
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
            .OrderByDescending(m => m.Fecha)
            .AsNoTracking();

        var totalCount = await baseQuery.LongCountAsync(cancellationToken);

        var movimientos = await baseQuery
            .Skip(query.PageIndex * query.PageSize)
            .Take(query.PageSize)
            .Select(m => new MovimientoDto(
                m.Id.Value,
                m.CuentaId.Value,
                m.TipoMovimiento,
                m.Fecha,
                m.Valor,
                m.Saldo))
            .ToListAsync(cancellationToken);

        var result = new PaginatedResult<MovimientoDto>(
            query.PageIndex, query.PageSize, totalCount, movimientos);

        return new GetMovimientosByCuentaResult(result);
    }
}