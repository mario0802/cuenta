namespace Client.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        // Add services to the container.
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<AuditableEntityInterceptor>();
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
        services.AddScoped<ILoginTokenGenerator, JwtLoginTokenGenerator>();


        services.AddDbContext<ClienteDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });
        services.AddHttpClient<IAccountServiceClient, AccountServiceClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["AccountService:BaseUrl"]!);
        });

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ClienteDbContext>());

        return services;
    }
}
