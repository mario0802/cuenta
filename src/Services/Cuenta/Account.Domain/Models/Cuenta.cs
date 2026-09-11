
namespace Account.Domain.Models;

public class Cuenta : Entity<CuentaId>
{
    private readonly List<Movimiento> _movimientos = new();

    public ClienteId ClienteId { get; private set; }
    public string NumeroCuenta { get; private set; }
    public TipoCuenta TipoCuenta { get; private set; }
    public decimal SaldoInicial { get; private set; }
    public EstadoCuenta Estado { get; private set; }

    public IReadOnlyCollection<Movimiento> Movimientos => _movimientos.AsReadOnly();

    private Cuenta() { }

    private Cuenta(CuentaId id, ClienteId clienteId, string numeroCuenta,
        TipoCuenta tipoCuenta, decimal saldoInicial, EstadoCuenta estado)
    {
        Id = id;
        ClienteId = clienteId;
        NumeroCuenta = numeroCuenta;
        TipoCuenta = tipoCuenta;
        SaldoInicial = saldoInicial;
        Estado = estado;
    }

    public static Cuenta Crear(ClienteId clienteId, string numeroCuenta,
        TipoCuenta tipoCuenta, decimal saldoInicial)
    {
        if (string.IsNullOrWhiteSpace(numeroCuenta))
            throw new DomainException("NumeroCuenta no puede ser vacío.");

        if (saldoInicial < 0)
            throw new DomainException("SaldoInicial no puede ser negativo.");

        return new Cuenta(CuentaId.New(), clienteId, numeroCuenta, tipoCuenta, saldoInicial, EstadoCuenta.Activa);
    }

    public void Actualizar(TipoCuenta tipoCuenta, EstadoCuenta estado)
    {
        TipoCuenta = tipoCuenta;
        Estado = estado;
    }

    public Movimiento RegistrarMovimiento(TipoMovimiento tipoMovimiento, decimal valor, DateTime fecha)
    {
        if (Estado != EstadoCuenta.Activa)
            throw new DomainException("No se pueden registrar movimientos en una cuenta inactiva.");

        if (valor <= 0)
            throw new DomainException("Valor debe ser mayor a cero.");

        SaldoInicial = tipoMovimiento switch
        {
            TipoMovimiento.Deposito => SaldoInicial + valor,
            TipoMovimiento.Retiro => SaldoInicial - valor,
            _ => throw new DomainException("Tipo de movimiento no soportado.")
        };

        if (SaldoInicial < 0)
            throw new DomainException("Saldo no disponible.");

        var movimiento = Movimiento.Crear(Id, tipoMovimiento, valor, SaldoInicial, fecha);
        _movimientos.Add(movimiento);
        return movimiento;
    }

    public void ActualizarMovimiento(MovimientoId movimientoId, TipoMovimiento tipoMovimiento,
        decimal valor, DateTime fecha)
    {
        if (Estado != EstadoCuenta.Activa)
            throw new DomainException("No se pueden modificar movimientos de una cuenta inactiva.");

        if (valor <= 0)
            throw new DomainException("Valor debe ser mayor a cero.");

        var movimiento = _movimientos.FirstOrDefault(m => m.Id == movimientoId)
            ?? throw new DomainException("El movimiento no pertenece a esta cuenta.");

        var saldoApertura = ObtenerSaldoApertura();

        movimiento.Actualizar(tipoMovimiento, valor, fecha);

        RecalcularSaldos(saldoApertura);
    }

    private decimal ObtenerSaldoApertura()
    {
        var primerMovimiento = _movimientos
            .OrderBy(m => m.Fecha)
            .ThenBy(m => m.Id.Value)
            .FirstOrDefault();

        if (primerMovimiento is null)
        {
            return SaldoInicial;
        }
        return primerMovimiento.TipoMovimiento switch
        {
            TipoMovimiento.Deposito => primerMovimiento.Saldo - primerMovimiento.Valor,
            TipoMovimiento.Retiro => primerMovimiento.Saldo + primerMovimiento.Valor,
            _ => throw new DomainException("Tipo de movimiento no soportado.")
        };
    }

    private void RecalcularSaldos(decimal saldoApertura)
    {
        var saldo = saldoApertura;

        foreach (var movimiento in _movimientos.OrderBy(m => m.Fecha).ThenBy(m => m.Id.Value))
        {
            saldo = movimiento.TipoMovimiento switch
            {
                TipoMovimiento.Deposito => saldo + movimiento.Valor,
                TipoMovimiento.Retiro => saldo - movimiento.Valor,
                _ => throw new DomainException("Tipo de movimiento no soportado.")
            };

            if (saldo < 0)
                throw new DomainException(
                    $"La actualización deja saldo negativo a partir del movimiento del {movimiento.Fecha:yyyy-MM-dd}.");

            movimiento.ActualizarSaldo(saldo);
        }

        SaldoInicial = saldo;
    }

    public void Activar() => Estado = EstadoCuenta.Activa;
    public void Inactivar() => Estado = EstadoCuenta.Inactiva;
}