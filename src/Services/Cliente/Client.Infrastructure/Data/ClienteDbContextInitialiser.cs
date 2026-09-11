using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace Client.Infrastructure.Data;

public static class ClienteDbContextInitialiser
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ClienteDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<ClienteDbContext>>();

        try
        {
            await context.Database.MigrateAsync();
            logger.LogInformation("Migraciones de {DbContext} aplicadas correctamente.", nameof(ClienteDbContext));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ocurrió un error al aplicar las migraciones de {DbContext}.", nameof(ClienteDbContext));
            throw;
        }
    }
}