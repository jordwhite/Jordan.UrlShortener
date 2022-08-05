using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Jordan.UrlShortener.Domain.Models;
using Jordan.UrlShortener.UserInterface.Api.Client.Requests;
using Jordan.UrlShortener.UserInterface.Api.Client.Responses;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Jordan.UrlShortener.UserInterface.Api.IntegrationTests
{
    public class GenerateControllerTests : BaseControllerTestClass
    {

        public GenerateControllerTests(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("https://stackoverflow.co/", true)]
        [InlineData("https://google.com/nice/page.html", true)]
        [InlineData("website", false)]
        [InlineData("google.com", false)]
        [InlineData("_____", false)]
        public async Task Given_AnyFullPath_WillGenerateShortenedUrlIfValid_And_WillAlwaysRedirectWhenValidOnGoTo(
            string fullUrl, bool isValid
        )
        {
            using var client = CreateNoRedirectClient();

            var request = new GenerateShortenedUrlRequest
            {
                FullUrl = fullUrl
            };

            var generateHttpResponse = await client.PostAsJsonAsync("Generate", request);
            var generateResponse = await generateHttpResponse.Content.ReadFromJsonAsync<GenerateShortenedUrlResponse>();

            if (isValid)
            {
                generateHttpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                generateResponse.Id.Should().NotBeNull();
            }
            else
            {
                generateHttpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                generateResponse.Id.Should().BeNull();
                return;
            }

            await AssertCorrectRedirect(generateResponse.Id, fullUrl, client);
        }

        [Fact]
        public async Task Given_AddedTooManyShortenedUrls_Then_WeAreThrottled()
        {
            using var client = CreateNoRedirectClient();

            var shortenedUrls = CreateManyShortenedUrlsForBanning(400, "::1");

            foreach (var shortenedUrl in shortenedUrls) 
                await AddOrUpdateShortenedUrl(shortenedUrl);

            var request = new GenerateShortenedUrlRequest
            {
                FullUrl = "https://google.com/"
            };

            var generateHttpResponse = await client.PostAsJsonAsync("Generate", request);
            generateHttpResponse.StatusCode.Should().Be(HttpStatusCode.TooManyRequests);

            DbContext.ShortenedUrls.RemoveRange(shortenedUrls);
            await DbContext.Save();

            var secondGenerateHttpResponse = await client.PostAsJsonAsync("Generate", request);
            secondGenerateHttpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private static ShortenedUrl[] CreateManyShortenedUrlsForBanning(int count, string clientIp)
        {
            var shortenedUrls = new List<ShortenedUrl>();

            for (var i = 0; i < count; i++)
            {
                shortenedUrls.Add(
                    new ShortenedUrl
                    {
                        Id = $"Generated-{i}",
                        FullUrl = "FullUrl",
                        ClientIp = clientIp,
                        CreatedOn = DateTime.UtcNow
                    }
                );
            }

            return shortenedUrls.ToArray();
        }

        private static async Task AssertCorrectRedirect(string shortenedUrlId, string fullUrl, HttpClient client)
        {
            var redirectHttpResponse = await client.GetAsync($"Go/{shortenedUrlId}");
            redirectHttpResponse.StatusCode.Should().Be(HttpStatusCode.MovedPermanently);

            var expectedUri = new Uri(fullUrl);
            var actualUri = redirectHttpResponse.Headers.Location;

            Assert.Equal(expectedUri, actualUri);
        }
    }
}
