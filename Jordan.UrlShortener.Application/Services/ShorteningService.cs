using Jordan.UrlShortener.Application.Generators;
using Jordan.UrlShortener.Application.Queries;
using Jordan.UrlShortener.Application.Repositories;
using Jordan.UrlShortener.Domain.Models;

namespace Jordan.UrlShortener.Application.Services
{
    public class ShorteningService : IShorteningService
    {
        private readonly IGetShortenedUrlQuery _getShortenedUrlQuery;
        private readonly IShortenedUrlRepository _shortenedUrlRepository;
        private readonly IRandomUniqueAlphaNumericIdGenerator _idGenerator;

        public ShorteningService(
            IGetShortenedUrlQuery getShortenedUrlQuery,
            IShortenedUrlRepository shortenedUrlRepository,
            IRandomUniqueAlphaNumericIdGenerator idGenerator
        )
        {
            _getShortenedUrlQuery = getShortenedUrlQuery;
            _shortenedUrlRepository = shortenedUrlRepository;
            _idGenerator = idGenerator;
        }

        public async Task<ShortenedUrl> Shorten(string fullUrl, string clientIp)
        {
            var existingShortenedUrl = await _getShortenedUrlQuery.GetByFullUrl(fullUrl);

            if (existingShortenedUrl != null)
                return existingShortenedUrl;

            var shortenedUrl = new ShortenedUrl
            {
                Id = await _idGenerator.Generate(),
                FullUrl = fullUrl,
                ClientIp = clientIp,
                CreatedOn = DateTime.UtcNow
            };

            await _shortenedUrlRepository.Add(shortenedUrl);

            return shortenedUrl;
        }
    }
}
