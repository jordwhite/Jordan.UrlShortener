using FluentValidation;
using FluentValidation.AspNetCore;
using Jordan.UrlShortener.UserInterface.Api.Client.Requests;
using Jordan.UrlShortener.UserInterface.Api.Validators;

namespace Jordan.UrlShortener.UserInterface.Api.Bootstrapping
{
    internal static class ValidatorsBootstrapping
    {
        public static IServiceCollection BootstrapValidators(this IServiceCollection services) =>
            services
                .AddScoped<IValidator<GenerateShortenedUrlRequest>, GenerateValidator>()
                .AddFluentValidationAutoValidation();
    }
}
