
namespace Account.Application.Cuentas.Commands.CreateCuenta;
public class CreateCuentaHandler(
    IApplicationDbContext dbContext,
    INumeroCuentaGenerator numeroCuentaGenerator)
    : ICommandHandler<CreateCuentaCommand, CreateCuentaResult>
{
    public async Task<CreateCuentaResult> Handle(CreateCuentaCommand command, CancellationToken cancellationToken)
    {
        var numeroCuenta = numeroCuentaGenerator.Generar();

        var cuenta = CreateNewCuenta(command.Cuenta, numeroCuenta);

        dbContext.Cuentas.Add(cuenta);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateCuentaResult(cuenta.Id.Value, cuenta.NumeroCuenta);
    }

    private Cuenta CreateNewCuenta(CuentaDto dto, string numeroCuenta)
    {
        var clienteId = ClienteId.Of(dto.ClienteId);

        return Cuenta.Crear(
            clienteId: clienteId,
            numeroCuenta: numeroCuenta,
            tipoCuenta: dto.TipoCuenta,
            saldoInicial: dto.SaldoInicial);
    }
}