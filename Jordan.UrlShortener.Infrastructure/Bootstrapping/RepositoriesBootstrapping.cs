using Jordan.UrlShortener.Application.Repositories;
using Jordan.UrlShortener.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Jordan.UrlShortener.Infrastructure.Bootstrapping
{
    internal static class RepositoriesBootstrapping
    {
        public static IServiceCollection BootstrapRepositories(this IServiceCollection services) =>
            services.AddScoped<IShortenedUrlRepository, ShortenedUrlRepository>();
    }
}
