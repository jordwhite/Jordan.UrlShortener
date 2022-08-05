using System.Net;
using FluentAssertions;
using Jordan.UrlShortener.Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jordan.UrlShortener.UserInterface.Api.IntegrationTests
{
    public class RedirectControllerTests : BaseControllerTestClass
    {

        public RedirectControllerTests(WebApplicationFactory<Program> factory) : base(factory) {}

        [Theory]
        [InlineData("abc")]
        [InlineData("creature")]
        public async Task Given_AnyShortenedUrlWithCharacters_When_GoToIsCalled_Then_AlwaysRedirectsToDefault(string shortenedUrlId)
        {
            using var client = CreateNoRedirectClient();

            var configuration = Factory.Services.GetService<IConfiguration>();
            var defaultUrl = configuration.GetValue<string>("DefaultRedirectUrl");

            var response = await client.GetAsync($"Go/{shortenedUrlId}");
            response.StatusCode.Should().Be(HttpStatusCode.MovedPermanently);

            var expectedUri = new Uri(defaultUrl);
            var actualUri = response.Headers.Location;

            Assert.Equal(expectedUri, actualUri);
        }

        [Theory]
        [InlineData("DerdfT44","https://google.co.uk/")]
        [InlineData("TIGYIDK322", "https://lg.co.uk")]
        public async Task Given_SpecifiedShortenedUrlIsInDb_When_GoToIsCalled_Then_AlwaysRedirectsToFullUrl(string shortenedUrlId, string fullUrl)
        {
            using var client = CreateNoRedirectClient();
            
            var shortenedUrl = new ShortenedUrl
            {
                Id = shortenedUrlId,
                FullUrl = fullUrl,
                ClientIp = "::1",
                CreatedOn = DateTime.UtcNow
            };

            await AddOrUpdateShortenedUrl(shortenedUrl);

            var response = await client.GetAsync($"Go/{shortenedUrlId}");
            
            response.StatusCode.Should().Be(HttpStatusCode.MovedPermanently);

            var expectedUri = new Uri(fullUrl);

            Assert.Equal(expectedUri, response.Headers.Location);
        }

    }

    
}
