namespace Account.Application.Movimientos.Commands.UpdateMovimiento;

public record UpdateMovimientoCommand(
    Guid CuentaId,
    Guid MovimientoId,
    TipoMovimiento TipoMovimiento,
    decimal Valor,
    DateTime? Fecha = null)
    : ICommand<UpdateMovimientoResult>;

public record UpdateMovimientoResult(Guid Id, decimal SaldoActual);

public class UpdateMovimientoCommandValidator : AbstractValidator<UpdateMovimientoCommand>
{
    public UpdateMovimientoCommandValidator()
    {
        RuleFor(x => x.CuentaId).NotEmpty().WithMessage("CuentaId es requerido");
        RuleFor(x => x.MovimientoId).NotEmpty().WithMessage("MovimientoId es requerido");
        RuleFor(x => x.TipoMovimiento).IsInEnum().WithMessage("TipoMovimiento no es válido");
        RuleFor(x => x.Valor).GreaterThan(0).WithMessage("Valor debe ser mayor a cero");
    }
}