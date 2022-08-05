using Jordan.UrlShortener.Infrastructure.Context;
using Jordan.UrlShortener.Infrastructure.LocalDb.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Jordan.UrlShortener.Infrastructure.LocalDb.Bootstrapping
{
    public static class LocalDbBootstrapping
    {
        public static IServiceCollection BootstrapLocalDb(
            this IServiceCollection services, string connectionString
        ) =>
            services
                .BootstrapDatabase(connectionString)
                .AddScoped<IDbContext, LocalDbContext>();
    }
}
