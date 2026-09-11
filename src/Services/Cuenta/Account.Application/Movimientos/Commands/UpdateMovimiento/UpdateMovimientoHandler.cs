
namespace Account.Application.Movimientos.Commands.UpdateMovimiento;

public class UpdateMovimientoHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateMovimientoCommand, UpdateMovimientoResult>
{
    public async Task<UpdateMovimientoResult> Handle(UpdateMovimientoCommand command, CancellationToken cancellationToken)
    {
        var cuentaId = CuentaId.Of(command.CuentaId);
        var movimientoId = MovimientoId.Of(command.MovimientoId);

        var cuenta = await dbContext.Cuentas
            .Include(c => c.Movimientos)
            .FirstOrDefaultAsync(c => c.Id == cuentaId, cancellationToken);

        if (cuenta is null)
            throw new NotFoundException($"No se ha encontrado la cuenta: {command.CuentaId}");

        if (cuenta.Movimientos.All(m => m.Id != movimientoId))
            throw new NotFoundException($"No se ha encontrado el movimiento: {command.MovimientoId}");

        var fecha = command.Fecha ?? DateTime.UtcNow;

        cuenta.ActualizarMovimiento(movimientoId, command.TipoMovimiento, command.Valor, fecha);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateMovimientoResult(movimientoId.Value, cuenta.SaldoInicial);
    }
}