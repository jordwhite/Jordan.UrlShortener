using Microsoft.Extensions.DependencyInjection;

namespace Jordan.UrlShortener.Application.Bootstrapping
{
    public static class ApplicationBootstrapping
    {
        public static IServiceCollection BootstrapApplication(this IServiceCollection services)
            => services
                .BootstrapMediator()
                .BootstrapServices()
                .BootstrapGenerators();
    }
}
