using Jordan.UrlShortener.Domain.Models;

namespace Jordan.UrlShortener.Application.Repositories
{
    public interface IShortenedUrlRepository
    {
        public Task Add(ShortenedUrl entity);
    }
}
