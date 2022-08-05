using Jordan.UrlShortener.Application.Bootstrapping;
using Jordan.UrlShortener.Infrastructure.Bootstrapping;
using Jordan.UrlShortener.Infrastructure.LocalDb.Bootstrapping;

namespace Jordan.UrlShortener.UserInterface.Api.Bootstrapping
{
    public static class ApiBootstrapping
    {
        public static IServiceCollection AddAllServices(
            this IServiceCollection services,
            ConfigurationManager configurationManager
        ) =>
            services
                .BootstrapApplication()
                .BootstrapInfrastructure()
                .BootstrapLocalDb(configurationManager.GetConnectionString("LocalDbConnection"))
                .BootstrapConfiguration(configurationManager)
                .BootstrapValidators();
    }
}
