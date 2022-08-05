using Jordan.UrlShortener.Application.CommandHandlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Jordan.UrlShortener.Application.Bootstrapping
{
    internal static class MediatorBootstrapping
    {
        public static IServiceCollection BootstrapMediator(this IServiceCollection services) => 
            services.AddMediatR(typeof(GenerateShortenedUrlCommandHandler));
    }
}
