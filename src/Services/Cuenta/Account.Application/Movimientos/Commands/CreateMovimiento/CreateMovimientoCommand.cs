namespace Account.Application.Movimientos.Commands.CreateMovimiento;

public record CreateMovimientoCommand(MovimientoDto Movimiento)
    : ICommand<CreateMovimientoResult>;

public record CreateMovimientoResult(Guid Id, decimal SaldoResultante);

public record MovimientoDto(
    Guid CuentaId,
    TipoMovimiento TipoMovimiento,
    decimal Valor,
    DateTime? Fecha = null);

public class CreateMovimientoCommandValidator : AbstractValidator<CreateMovimientoCommand>
{
    public CreateMovimientoCommandValidator()
    {
        RuleFor(x => x.Movimiento.CuentaId)
            .NotEmpty().WithMessage("CuentaId es requerido");

        RuleFor(x => x.Movimiento.TipoMovimiento)
            .IsInEnum().WithMessage("TipoMovimiento no es válido");

        RuleFor(x => x.Movimiento.Valor)
            .GreaterThan(0).WithMessage("Valor debe ser mayor a cero");
    }
}