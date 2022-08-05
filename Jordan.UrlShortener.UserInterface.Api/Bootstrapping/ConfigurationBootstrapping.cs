using Jordan.UrlShortener.Application.Bootstrapping;
using Jordan.UrlShortener.Application.Configuration;

namespace Jordan.UrlShortener.UserInterface.Api.Bootstrapping
{
    internal static class ConfigurationBootstrapping
    {
        public static IServiceCollection BootstrapConfiguration(
            this IServiceCollection services,
            ConfigurationManager configurationManager
        ) =>
            services
                .AddSingleton<IApplicationOptions, ApplicationOptions>(provider => new ApplicationOptions()
                {
                    DefaultRedirectUrl = configurationManager.GetValue<string>("DefaultRedirectUrl"),
                    MaxSubmissionsPerHour = configurationManager.GetValue<int>("MaxSubmissionsPerHour"),
                    MaxUniqueRandomIdGenerateRetries = configurationManager.GetValue<int>("MaxUniqueRandomIdGenerateRetries"),
                    ShortUrlIdMaxLength = configurationManager.GetValue<int>("ShortUrlIdMaxLength"),
                    ShortUrlIdMinLength = configurationManager.GetValue<int>("ShortUrlIdMinLength")
                });
    }
}
