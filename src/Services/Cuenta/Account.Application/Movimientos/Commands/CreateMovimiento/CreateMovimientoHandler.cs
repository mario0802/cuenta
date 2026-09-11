namespace Account.Application.Movimientos.Commands.CreateMovimiento;

public class CreateMovimientoHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateMovimientoCommand, CreateMovimientoResult>
{
    public async Task<CreateMovimientoResult> Handle(CreateMovimientoCommand command, CancellationToken cancellationToken)
    {
        var cuentaId = CuentaId.Of(command.Movimiento.CuentaId);

        var cuenta = await dbContext.Cuentas
            .FirstOrDefaultAsync(c => c.Id == cuentaId, cancellationToken);

        if (cuenta is null)
        {
            throw new NotFoundException($"No se ha encontrado la cuenta con ID {command.Movimiento.CuentaId}");
        }

        var fecha = command.Movimiento.Fecha ?? DateTime.UtcNow;

        var movimiento = cuenta.RegistrarMovimiento(
            tipoMovimiento: command.Movimiento.TipoMovimiento,
            valor: command.Movimiento.Valor,
            fecha: fecha);

        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateMovimientoResult(movimiento.Id.Value, cuenta.SaldoInicial);
    }
}