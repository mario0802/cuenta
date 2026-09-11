namespace Account.Application.Data;

public interface IApplicationDbContext
{
    DbSet<Cuenta> Cuentas { get; }
    DbSet<Movimiento> Movimientos { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

