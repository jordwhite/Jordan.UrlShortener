using Jordan.UrlShortener.Domain.Models;

namespace Jordan.UrlShortener.Application.Queries
{
    public interface IGetShortenedUrlQuery
    {
        Task<ShortenedUrl> GetByFullUrl(string fullUrl);
        Task<ShortenedUrl> GetById(string id);
    }
}
