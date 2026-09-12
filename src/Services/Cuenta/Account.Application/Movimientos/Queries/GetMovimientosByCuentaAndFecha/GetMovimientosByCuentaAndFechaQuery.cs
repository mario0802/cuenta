namespace Account.Application.Movimientos.Queries.GetMovimientosByCuentaAndFecha;

public record GetMovimientosByCuentaAndFechaQuery(
    Guid CuentaId,
    DateTime? FechaDesde = null,
    DateTime? FechaHasta = null)
    : IQuery<GetMovimientosByCuentaAndFechaResult>;

public record GetMovimientosByCuentaAndFechaResult(List<MovimientoDto> Movimientos);

public record MovimientoDto(
    Guid Id,
    Guid CuentaId,
    TipoMovimiento TipoMovimiento,
    DateTime Fecha,
    decimal Valor,
    decimal Saldo);

public class GetMovimientosByCuentaAndFechaQueryValidator : AbstractValidator<GetMovimientosByCuentaAndFechaQuery>
{
    public GetMovimientosByCuentaAndFechaQueryValidator()
    {
        RuleFor(x => x.CuentaId)
            .NotEmpty().WithMessage("CuentaId es requerido");

        RuleFor(x => x.FechaHasta)
            .GreaterThanOrEqualTo(x => x.FechaDesde)
            .When(x => x.FechaDesde.HasValue && x.FechaHasta.HasValue)
            .WithMessage("FechaHasta debe ser mayor o igual a FechaDesde");
    }
}