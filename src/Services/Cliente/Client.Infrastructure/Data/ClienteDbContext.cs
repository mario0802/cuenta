using System.Reflection;

namespace Client.Infrastructure.Data;

public class ClienteDbContext : DbContext, IApplicationDbContext
{
    private readonly AuditableEntityInterceptor? _auditableEntityInterceptor;

    public ClienteDbContext(
        DbContextOptions<ClienteDbContext> options,
        AuditableEntityInterceptor? auditableEntityInterceptor = null)
        : base(options)
    {
        _auditableEntityInterceptor = auditableEntityInterceptor;
    }

    public DbSet<Cliente> Clientes => Set<Cliente>();

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