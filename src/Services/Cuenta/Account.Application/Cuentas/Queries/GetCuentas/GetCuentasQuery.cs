
namespace Account.Application.Cuentas.Queries.GetCuentas;

public record GetCuentasQuery(PaginationRequest PaginationRequest)
    : IQuery<GetCuentasResult>;

public record GetCuentasResult(PaginatedResult<CuentaResumenDto> Cuentas);

public record CuentaResumenDto(
    Guid Id,
    Guid ClienteId,
    string NumeroCuenta,
    TipoCuenta TipoCuenta,
    decimal SaldoInicial,
    EstadoCuenta Estado);