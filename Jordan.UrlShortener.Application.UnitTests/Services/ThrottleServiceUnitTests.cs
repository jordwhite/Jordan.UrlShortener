using Jordan.UrlShortener.Application.Configuration;
using Jordan.UrlShortener.Application.Queries;
using Jordan.UrlShortener.Application.Services;
using Moq;

namespace Jordan.UrlShortener.Application.UnitTests.Services
{
    public class ThrottleServiceUnitTests
    {
        [Theory]
        [InlineData(5, 10, false)]
        [InlineData(9, 10, false)]
        [InlineData(10, 10, false)]
        [InlineData(11, 10, true)]
        [InlineData(11, 100, false)]
        public async Task Given_Various_ClientIpSubmissionCountValues_And_MaxSubmissionsPerHourValues_Then_ReturnsTrueIfExceededAndFalseIfNot(int submissionCount, int maxPerHour, bool expected)
        {
            const string validClientIp = "12345";

            var countAllShortenedUrlsByClientIpWithinLastNSecondsQueryMock =
                new Mock<ICountAllShortenedUrlsByClientIpWithinLastNSecondsQuery>(
                    MockBehavior.Strict
                );

            var applicationOptionsMock = 
                new Mock<IApplicationOptions>(MockBehavior.Strict);

            countAllShortenedUrlsByClientIpWithinLastNSecondsQueryMock
                .Setup(query => query.CountAll(
                    It.Is<string>(str => str == validClientIp), 
                    It.Is<double>(secs => Math.Abs(secs - (double)60 * 60) < double.Epsilon)
                ))
                .ReturnsAsync(submissionCount)
                .Verifiable();

            applicationOptionsMock
                .SetupGet(options => options.MaxSubmissionsPerHour)
                .Returns(maxPerHour)
                .Verifiable();

            var serviceUnderTest = new ThrottleService(
                countAllShortenedUrlsByClientIpWithinLastNSecondsQueryMock.Object,
                applicationOptionsMock.Object
            );

            var result = await serviceUnderTest.ShouldThrottleClientIp(validClientIp);

            Assert.Equal(expected, result);

            countAllShortenedUrlsByClientIpWithinLastNSecondsQueryMock.Verify();
            applicationOptionsMock.Verify();

            countAllShortenedUrlsByClientIpWithinLastNSecondsQueryMock.VerifyNoOtherCalls();
            applicationOptionsMock.VerifyNoOtherCalls();

            
        }
    }
}
