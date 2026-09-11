
namespace Client.Application.Clientes.Commands.DeleteCliente;

public record DeleteClienteCommand(Guid Id)
    : ICommand<DeleteClienteResult>;

public record DeleteClienteResult(bool IsSuccess);

public class DeleteClienteCommandValidator : AbstractValidator<DeleteClienteCommand>
{
    public DeleteClienteCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id es requerido");
    }
}