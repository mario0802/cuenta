using System.Reflection;

namespace Account.Infrastructure.Data;

public class AccountDbContext : DbContext, IApplicationDbContext
{
    private readonly AuditableEntityInterceptor? _auditableEntityInterceptor;

    public AccountDbContext(
        DbContextOptions<AccountDbContext> options,
        AuditableEntityInterceptor? auditableEntityInterceptor = null)
        : base(options)
    {
        _auditableEntityInterceptor = auditableEntityInterceptor;
    }

    public DbSet<Cuenta> Cuentas => Set<Cuenta>();
    public DbSet<Movimiento> Movimientos => Set<Movimiento>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_auditableEntityInterceptor is not null)
        {
            optionsBuilder.AddInterceptors(_auditableEntityInterceptor);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}