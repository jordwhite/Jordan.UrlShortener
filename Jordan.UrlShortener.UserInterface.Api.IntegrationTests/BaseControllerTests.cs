using Jordan.UrlShortener.Domain.Models;
using Jordan.UrlShortener.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Jordan.UrlShortener.UserInterface.Api.IntegrationTests
{
    public abstract class BaseControllerTestClass : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly WebApplicationFactory<Program> Factory;
        protected IServiceScope Scope;
        protected IDbContext DbContext;

        protected BaseControllerTestClass(WebApplicationFactory<Program> factory)
        {
            Factory = factory;
            Scope = Factory.Services.CreateScope();
            DbContext = Scope.ServiceProvider.GetRequiredService<IDbContext>();
        }

        protected HttpClient CreateNoRedirectClient() =>
            Factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false,
                    BaseAddress = Factory.Server.BaseAddress,
                    HandleCookies = true,
                    MaxAutomaticRedirections = 0
                }
            );

        protected HttpClient CreateNormalClient() =>
            Factory.CreateClient();

        protected async Task AddOrUpdateShortenedUrl(ShortenedUrl shortenedUrl)
        {
            var shouldUpdate = DbContext.ShortenedUrls.Any(url => url.Id == shortenedUrl.Id);

            if (shouldUpdate)
                DbContext.ShortenedUrls.Update(shortenedUrl);
            else
                DbContext.ShortenedUrls.Add(shortenedUrl);

            await DbContext.Save();
        }

    }


}