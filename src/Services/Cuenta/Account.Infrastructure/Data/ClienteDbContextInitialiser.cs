using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace Account.Infrastructure.Data;

public static class ClienteDbContextInitialiser
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AccountDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<AccountDbContext>>();

        try
        {
            await context.Database.MigrateAsync();
            logger.LogInformation("Migraciones de {DbContext} aplicadas correctamente.", nameof(AccountDbContext));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ocurrió un error al aplicar las migraciones de {DbContext}.", nameof(AccountDbContext));
            throw;
        }
    }
}