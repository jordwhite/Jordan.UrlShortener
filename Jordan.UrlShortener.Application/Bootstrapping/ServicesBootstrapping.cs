using Jordan.UrlShortener.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Jordan.UrlShortener.Application.Bootstrapping
{
    internal static class ServicesBootstrapping
    {
        public static IServiceCollection BootstrapServices(this IServiceCollection services) =>
            services
                .AddScoped<IShorteningService, ShorteningService>()
                .AddScoped<IUrlRetrievalService, UrlRetrievalService>()
                .AddScoped<IThrottleService, ThrottleService>()
                .AddSingleton<IRandomSeedProvider, RandomSeedProvider>();
    }
}
