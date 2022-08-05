using Microsoft.Extensions.DependencyInjection;

namespace Jordan.UrlShortener.Infrastructure.Bootstrapping
{
    public static class InfrastructureBootstrapping
    {
        public static IServiceCollection BootstrapInfrastructure(this IServiceCollection services) =>
            services
                .BootstrapRepositories()
                .BootstrapQueries();

    }
}
