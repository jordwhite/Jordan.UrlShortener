using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jordan.UrlShortener.Application.Queries;
using Jordan.UrlShortener.Application.Services;
using Jordan.UrlShortener.Domain.Models;
using Moq;

namespace Jordan.UrlShortener.Application.UnitTests.Services
{
    public class UrlRetrievalServiceUnitTests
    {
        [Fact]
        public async Task Given_ValidShortenedUrlId_Then_Returns_ValidFullUrl()
        {
            var getShortenedUrlQueryMock = new Mock<IGetShortenedUrlQuery>();

            const string validShortenedUrlId = "valid-id";

            var shortenedUrl = new ShortenedUrl
            {
                Id = validShortenedUrlId,
                ClientIp = "ClientIp",
                CreatedOn = DateTime.UnixEpoch,
                FullUrl = "FullUrl"
            };

            getShortenedUrlQueryMock.Setup(
                query => query.GetById(It.Is<string>(str => str == validShortenedUrlId))
            ).ReturnsAsync(() => shortenedUrl)
                .Verifiable();

            var serviceUnderTest = new UrlRetrievalService(getShortenedUrlQueryMock.Object);
            var result = await serviceUnderTest.Retrieve(validShortenedUrlId);

            Assert.Equal(shortenedUrl.FullUrl, result);

            getShortenedUrlQueryMock.Verify();
            getShortenedUrlQueryMock.VerifyNoOtherCalls();
        }
    }
}
