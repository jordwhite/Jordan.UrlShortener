using Jordan.UrlShortener.Application.Generators;
using Microsoft.Extensions.DependencyInjection;

namespace Jordan.UrlShortener.Application.Bootstrapping
{ 
    internal static class GeneratorsBootstrapping
    {
        public static IServiceCollection BootstrapGenerators(this IServiceCollection services) =>
            services
                .AddSingleton<IRandomAlphaNumericIdGenerator, RandomAlphaNumericIdGenerator>()
                .AddScoped<IRandomUniqueAlphaNumericIdGenerator, RandomUniqueAlphaNumericIdGenerator>();
    }
}
