
namespace Account.Application.Cuentas.Commands.UpdateCuenta;

public class UpdateCuentaHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateCuentaCommand, UpdateCuentaResult>
{
    public async Task<UpdateCuentaResult> Handle(UpdateCuentaCommand command, CancellationToken cancellationToken)
    {
        var cuentaId = CuentaId.Of(command.Id);

        var cuenta = await dbContext.Cuentas
            .FirstOrDefaultAsync(c => c.Id == cuentaId, cancellationToken);

        if (cuenta is null)
        {
            throw new NotFoundException(nameof(Cuenta), command.Id);
        }

        cuenta.Actualizar(command.Cuenta.TipoCuenta, command.Cuenta.Estado);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateCuentaResult(true);
    }
}