using Jordan.UrlShortener.Application.Generators;
using Jordan.UrlShortener.Application.Queries;
using Jordan.UrlShortener.Application.Repositories;
using Jordan.UrlShortener.Application.Services;
using Jordan.UrlShortener.Domain.Models;
using Moq;

namespace Jordan.UrlShortener.Application.UnitTests.Services
{
    public class ShorteningServiceUnitTests
    {
        [Fact]
        public async Task Given_ValidFullUrlThatDoesNotAlreadyExist_And_ValidClientIp_When_ShortenIsCalled_Then_ReturnsValidShortenedUrlThatHasBeenAdded()
        {
            const string validFullUrl = "https://microsoft.com/";
            const string validClientIp = "12345";

            var alreadyExistingShortenedUrl = new ShortenedUrl
            {
                Id = "Id",
                ClientIp = "ClientIp",
                CreatedOn = DateTime.UnixEpoch,
                FullUrl = "FullUrl"
            };

            var getShortenedUrlQueryMock = new Mock<IGetShortenedUrlQuery>(MockBehavior.Strict);
            var shortenedUrlRepositoryMock = new Mock<IShortenedUrlRepository>(MockBehavior.Strict);
            var randomUniqueAlphaNumericIdGeneratorMock = new Mock<IRandomUniqueAlphaNumericIdGenerator>(MockBehavior.Strict);

            getShortenedUrlQueryMock
                .Setup(query => query.GetByFullUrl(
                    It.Is<string>(str => str == validFullUrl)
                )).ReturnsAsync(() => alreadyExistingShortenedUrl).Verifiable();

            var shorteningService = new ShorteningService(
                getShortenedUrlQueryMock.Object,
                shortenedUrlRepositoryMock.Object,
                randomUniqueAlphaNumericIdGeneratorMock.Object
            );

            var result = await shorteningService.Shorten(validFullUrl, validClientIp);

            Assert.Equal(alreadyExistingShortenedUrl, result);

            getShortenedUrlQueryMock.Verify();
            shortenedUrlRepositoryMock.Verify();
            randomUniqueAlphaNumericIdGeneratorMock.Verify();

            getShortenedUrlQueryMock.VerifyNoOtherCalls();
            shortenedUrlRepositoryMock.VerifyNoOtherCalls();
            randomUniqueAlphaNumericIdGeneratorMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Given_ValidFullUrlThatDoesAlreadyExist_And_ValidClientIp_When_ShortenIsCalled_Then_ReturnsValidShortenedUrl()
        {
            const string validFullUrl = "https://microsoft.com/";
            const string validClientIp = "12345";
            const string validId = "valid-id";

            var getShortenedUrlQueryMock = new Mock<IGetShortenedUrlQuery>(MockBehavior.Strict);
            var shortenedUrlRepositoryMock = new Mock<IShortenedUrlRepository>(MockBehavior.Strict);
            var randomUniqueAlphaNumericIdGeneratorMock = new Mock<IRandomUniqueAlphaNumericIdGenerator>(MockBehavior.Strict);

            getShortenedUrlQueryMock
                .Setup(query => query.GetByFullUrl(
                    It.Is<string>(str => str == validFullUrl)
                )).ReturnsAsync(() => null).Verifiable();

            randomUniqueAlphaNumericIdGeneratorMock
                .Setup(generator => generator.Generate())
                .ReturnsAsync(validId)
                .Verifiable();

            shortenedUrlRepositoryMock.Setup(
                repo => repo.Add(
                    It.Is<ShortenedUrl>(url => validId == url.Id &&
                                               validFullUrl == url.FullUrl &&
                                               validClientIp == url.ClientIp)
                )
            ).Returns(() => Task.CompletedTask).Verifiable();

            var shorteningService = new ShorteningService(
                getShortenedUrlQueryMock.Object,
                shortenedUrlRepositoryMock.Object,
                randomUniqueAlphaNumericIdGeneratorMock.Object
            );

            var result = await shorteningService.Shorten(validFullUrl, validClientIp);

            Assert.Equal(validId, result.Id);
            Assert.Equal(validFullUrl, result.FullUrl);
            Assert.Equal(validClientIp, result.ClientIp);

            getShortenedUrlQueryMock.Verify();
            shortenedUrlRepositoryMock.Verify();
            randomUniqueAlphaNumericIdGeneratorMock.Verify();

            getShortenedUrlQueryMock.VerifyNoOtherCalls();
            shortenedUrlRepositoryMock.VerifyNoOtherCalls();
            randomUniqueAlphaNumericIdGeneratorMock.VerifyNoOtherCalls();
        }
    }
}
