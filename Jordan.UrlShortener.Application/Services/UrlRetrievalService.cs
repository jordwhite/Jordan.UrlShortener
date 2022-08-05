using Jordan.UrlShortener.Application.Queries;

namespace Jordan.UrlShortener.Application.Services
{
    public class UrlRetrievalService : IUrlRetrievalService
    {
        private readonly IGetShortenedUrlQuery _getShortenedUrlQuery;

        public UrlRetrievalService(IGetShortenedUrlQuery getShortenedUrlQuery)
        {
            _getShortenedUrlQuery = getShortenedUrlQuery;
        }

        public async Task<string> Retrieve(string shortenedUrlId)
        {
            var entity = await _getShortenedUrlQuery.GetById(shortenedUrlId);
            return entity?.FullUrl;
        }
    }
}
