using Jordan.UrlShortener.UserInterface.Api.Client.Requests;
using Jordan.UrlShortener.UserInterface.Api.Validators;

namespace Jordan.UrlShortener.UserInterface.Api.UnitTests.Validators
{
    public class GenerateValidatorUnitTests
    {
        [Theory]
        [InlineData("string")]
        [InlineData("google.com/abc")]
        [InlineData("google.co.uk/")]
        [InlineData("//google.co.uk/dir1/dir2")]
        [InlineData("ftp//google.co.uk/")]
        [InlineData("http//google.co.uk/abc")]
        [InlineData("https://google.co.uk/!~\\!")]
        public void Given_InvalidFullUrlThatIsNotEmptyOrWhitespaceOrNull_When_ValidateIsCalled_Then_ValidateResultContainsErrors(string fullUrl)
        {
            var generateValidator = new GenerateValidator();

            var request = new GenerateShortenedUrlRequest
            {
                FullUrl = fullUrl
            };

            var result = generateValidator.Validate(request);

            var errorMessages = result.Errors.Select(failure => failure.ErrorMessage);

            Assert.Single(result.Errors);

            Assert.Contains(
                "Must be a well formed absolute URI with a valid protocol e.g. 'https://google.co.uk/'",
                errorMessages
            );
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(" ")]
        [InlineData("     ")]
        public void Given_InvalidFullUrlThatIsEmptyOrWhitespaceOrNull_When_ValidateIsCalled_Then_ValidateResultContainsErrors(string fullUrl)
        {
            var generateValidator = new GenerateValidator();

            var request = new GenerateShortenedUrlRequest
            {
                FullUrl = fullUrl
            };

            var result = generateValidator.Validate(request);

            var errorMessages = result.Errors.Select(failure => failure.ErrorMessage);

            Assert.Equal(2, result.Errors.Count);

            Assert.Contains(
                "Must be a well formed absolute URI with a valid protocol e.g. 'https://google.co.uk/'",
                errorMessages
            );

            Assert.Contains(
                "'Full Url' must not be empty.",
                errorMessages
            );
        }

        [Theory]
        [InlineData("https://google.com/")]
        [InlineData("https://microsoft.com/")]
        [InlineData("ftp://microsoft.com/directory/structure/is/here")]
        public void Given_ValidFullUrl_When_ValidateIsCalled_Then_ValidateResultContainsNoErrors(string fullUrl)
        {
            var generateValidator = new GenerateValidator();

            var request = new GenerateShortenedUrlRequest
            {
                FullUrl = fullUrl
            };

            var result = generateValidator.Validate(request);

            Assert.Empty(result.Errors);

        }
    }
}
