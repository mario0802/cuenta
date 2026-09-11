namespace Account.Domain.Models;

public class Movimiento : Entity<MovimientoId>
{
    public CuentaId CuentaId { get; private set; }
    public TipoMovimiento TipoMovimiento { get; private set; }
    public DateTime Fecha { get; private set; }
    public decimal Valor { get; private set; }
    public decimal Saldo { get; private set; }

    private Movimiento() { }

    private Movimiento(MovimientoId id, CuentaId cuentaId, TipoMovimiento tipoMovimiento,
        decimal valor, decimal saldo, DateTime fecha)
    {
        Id = id;
        CuentaId = cuentaId;
        TipoMovimiento = tipoMovimiento;
        Valor = valor;
        Saldo = saldo;
        Fecha = fecha;
    }

    public static Movimiento Crear(CuentaId cuentaId, TipoMovimiento tipoMovimiento,
        decimal valor, decimal saldoResultante, DateTime fecha)
    {
        return new Movimiento(MovimientoId.New(), cuentaId, tipoMovimiento, valor, saldoResultante, fecha);
    }

    internal void Actualizar(TipoMovimiento tipoMovimiento, decimal valor, DateTime fecha)
    {
        if (valor <= 0)
        {
            throw new DomainException("Valor debe ser mayor a cero.");
        }

        TipoMovimiento = tipoMovimiento;
        Valor = valor;
        Fecha = fecha;
    }

    internal void ActualizarSaldo(decimal saldo)
    {
        Saldo = saldo;
    }
}