using Account.Application.Common.Models;

namespace Account.Application.Reportes.GetDetalleCuentasByCliente;

public record GetDetalleCuentasByClienteQuery(
    Guid ClienteId,
    DateTime? FechaDesde = null,
    DateTime? FechaHasta = null)
    : IQuery<GetDetalleCuentasByClienteResult>;

public record GetDetalleCuentasByClienteResult(
    ClienteDto Cliente,
    List<CuentaMovimientosDto> Cuentas);

public record CuentaMovimientosDto(
    Guid CuentaId,
    string NumeroCuenta,
    decimal SaldoInicial,
    List<MovimientoDto> Movimientos);

public record MovimientoDto(
    Guid Id,
    TipoMovimiento TipoMovimiento,
    DateTime Fecha,
    decimal Valor,
    decimal Saldo);

public class GetDetalleCuentasByClienteQueryValidator : AbstractValidator<GetDetalleCuentasByClienteQuery>
{
    public GetDetalleCuentasByClienteQueryValidator()
    {
        RuleFor(x => x.ClienteId)
            .NotEmpty().WithMessage("ClienteId es requerido");

        RuleFor(x => x.FechaHasta)
            .GreaterThanOrEqualTo(x => x.FechaDesde)
            .When(x => x.FechaDesde.HasValue && x.FechaHasta.HasValue)
            .WithMessage("FechaHasta debe ser mayor o igual a FechaDesde");
    }
}