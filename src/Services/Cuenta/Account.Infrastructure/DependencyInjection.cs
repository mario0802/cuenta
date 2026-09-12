

using BuildingBlocks.Auth;

namespace Account.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        // Add services to the container.
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<AuditableEntityInterceptor>();
        services.AddScoped<INumeroCuentaGenerator, NumeroCuentaGenerator>();
        services.AddDbContext<AccountDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<AccountDbContext>());

        services.AddHttpClient<IClientServiceClient, ClientServiceClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["ClientService:BaseUrl"]!);
        });

        return services;
    }
}