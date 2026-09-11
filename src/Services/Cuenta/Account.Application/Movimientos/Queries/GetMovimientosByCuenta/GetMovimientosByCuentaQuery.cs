namespace Account.Application.Movimientos.Queries.GetMovimientosByCuenta;

public record GetMovimientosByCuentaQuery(
    Guid CuentaId,
    int PageIndex = 0,
    int PageSize = 10)
    : IQuery<GetMovimientosByCuentaResult>;

public record GetMovimientosByCuentaResult(PaginatedResult<MovimientoDto> Movimientos);

public record MovimientoDto(
    Guid Id,
    Guid CuentaId,
    TipoMovimiento TipoMovimiento,
    DateTime Fecha,
    decimal Valor,
    decimal Saldo);

public class GetMovimientosByCuentaQueryValidator : AbstractValidator<GetMovimientosByCuentaQuery>
{
    public GetMovimientosByCuentaQueryValidator()
    {
        RuleFor(x => x.CuentaId)
            .NotEmpty().WithMessage("CuentaId es requerido");

        RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(0).WithMessage("PageIndex no puede ser negativo");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("PageSize debe estar entre 1 y 100");
    }
}