using Jordan.UrlShortener.Application.Configuration;
using Jordan.UrlShortener.Application.Queries;

namespace Jordan.UrlShortener.Application.Generators
{
    public class RandomUniqueAlphaNumericIdGenerator : IRandomUniqueAlphaNumericIdGenerator
    {
        private readonly IRandomAlphaNumericIdGenerator _randomAlphaNumericIdGenerator;
        private readonly IGetShortenedUrlQuery _getShortenedUrlQuery;
        private readonly IApplicationOptions _applicationOptions;

        public RandomUniqueAlphaNumericIdGenerator(
            IRandomAlphaNumericIdGenerator randomAlphaNumericIdGenerator,
            IGetShortenedUrlQuery getShortenedUrlQuery,
            IApplicationOptions applicationOptions
        )
        {
            _randomAlphaNumericIdGenerator = randomAlphaNumericIdGenerator;
            _getShortenedUrlQuery = getShortenedUrlQuery;
            _applicationOptions = applicationOptions;
        }

        public async Task<string> Generate()
        {
            for (var i = 0; i < _applicationOptions.MaxUniqueRandomIdGenerateRetries; i++)
            {
                var randomId = _randomAlphaNumericIdGenerator.Generate();
                var existingShortenedUrl = await _getShortenedUrlQuery.GetById(randomId);

                if (existingShortenedUrl == null)
                    return randomId;
            }

            // This is very, very rare.
            throw new InvalidOperationException(
                "Attempted to generate a unique random ID for a shortened URL but failed " +
                $"as it was non-unique more than {_applicationOptions.MaxUniqueRandomIdGenerateRetries} time(s)."
            );
        }
    }
}
