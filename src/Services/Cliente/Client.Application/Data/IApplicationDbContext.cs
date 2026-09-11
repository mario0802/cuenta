
namespace Client.Application.Data;

public interface IApplicationDbContext
{
    DbSet<Cliente> Clientes { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

