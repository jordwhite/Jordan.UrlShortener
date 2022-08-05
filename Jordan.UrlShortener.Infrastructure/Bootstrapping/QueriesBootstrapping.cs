using Jordan.UrlShortener.Application.Queries;
using Jordan.UrlShortener.Infrastructure.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Jordan.UrlShortener.Infrastructure.Bootstrapping
{
    internal static class QueriesBootstrapping
    {
        public static IServiceCollection BootstrapQueries(this IServiceCollection services) =>
            services
                .AddScoped<IGetShortenedUrlQuery, GetShortenedUrlQuery>()
                .AddScoped<
                    ICountAllShortenedUrlsByClientIpWithinLastNSecondsQuery,
                    CountAllShortenedUrlsByClientIpWithinLastNSecondsQuery
                >();
    }
}
