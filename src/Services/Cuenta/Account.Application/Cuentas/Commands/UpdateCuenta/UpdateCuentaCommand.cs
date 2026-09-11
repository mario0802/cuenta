
namespace Account.Application.Cuentas.Commands.UpdateCuenta;

public record UpdateCuentaCommand(Guid Id, UpdateCuentaDto Cuenta)
    : ICommand<UpdateCuentaResult>;

public record UpdateCuentaResult(Guid Id);

public record UpdateCuentaDto(
    TipoCuenta TipoCuenta,
    EstadoCuenta Estado);

public class UpdateCuentaCommandValidator : AbstractValidator<UpdateCuentaCommand>
{
    public UpdateCuentaCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id es requerido");

        RuleFor(x => x.Cuenta.TipoCuenta)
            .IsInEnum().WithMessage("TipoCuenta no es válido");

        RuleFor(x => x.Cuenta.Estado)
            .IsInEnum().WithMessage("Estado no es válido");
    }
}