namespace Client.Application.Clientes.Commands.UpdateCliente;

public record UpdateClienteCommand(UpdateClienteDto Cliente)
    : ICommand<UpdateClienteResult>;

public record UpdateClienteResult(bool IsSuccess);

public class UpdateClienteCommandValidator : AbstractValidator<UpdateClienteCommand>
{
    public UpdateClienteCommandValidator()
    {
        RuleFor(x => x.Cliente.Id).NotEmpty().WithMessage("Id es requerido");
        RuleFor(x => x.Cliente.Nombre).NotEmpty().WithMessage("Nombre es requerido");
        RuleFor(x => x.Cliente.Edad).GreaterThan(0).WithMessage("Edad debe ser mayor que 0");
        RuleFor(x => x.Cliente.Direccion).NotEmpty().WithMessage("Direccion es requerida");
        RuleFor(x => x.Cliente.Telefono).NotEmpty().WithMessage("Telefono es requerido");
    }
}