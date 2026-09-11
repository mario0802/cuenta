namespace Account.Application.Cuentas.Commands.CreateCuenta;

public record CreateCuentaCommand(CuentaDto Cuenta)
    : ICommand<CreateCuentaResult>;

public record CreateCuentaResult(Guid Id, string numeroCuenta);

public record CuentaDto(
    Guid ClienteId,
    TipoCuenta TipoCuenta,
    decimal SaldoInicial);

public class CreateCuentaCommandValidator : AbstractValidator<CreateCuentaCommand>
{
    public CreateCuentaCommandValidator()
    {
        RuleFor(x => x.Cuenta.ClienteId)
            .NotEmpty().WithMessage("ClienteId es requerido");

        RuleFor(x => x.Cuenta.TipoCuenta)
            .IsInEnum().WithMessage("TipoCuenta no es válido");

        RuleFor(x => x.Cuenta.SaldoInicial)
            .GreaterThanOrEqualTo(0).WithMessage("SaldoInicial no puede ser negativo");
    }
}