

namespace Client.Application.Clientes.Commands.CreateCliente;

public record CreateClienteCommand(ClienteDto Cliente)
    : ICommand<CreateClienteResult>;

public record CreateClienteResult(Guid Id);

public class CreateClienteCommandValidator : AbstractValidator<CreateClienteCommand>
{
    public CreateClienteCommandValidator()
    {
        RuleFor(x => x.Cliente.Nombre).NotEmpty().WithMessage("Nombre es requerido");
        RuleFor(x => x.Cliente.Identificacion).NotEmpty().WithMessage("Identificacion es requerida");
        RuleFor(x => x.Cliente.Edad).GreaterThan(0).WithMessage("Edad debe ser mayor que 0");
        RuleFor(x => x.Cliente.Direccion).NotEmpty().WithMessage("Direccion es requerida");
        RuleFor(x => x.Cliente.Telefono).NotEmpty().WithMessage("Telefono es requerido");
        RuleFor(x => x.Cliente.Password)
            .NotEmpty().WithMessage("Password es requerida")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters");
    }
}