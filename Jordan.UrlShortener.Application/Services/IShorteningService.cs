using Jordan.UrlShortener.Domain.Models;

namespace Jordan.UrlShortener.Application.Services;

public interface IShorteningService
{
    Task<ShortenedUrl> Shorten(string fullUrl, string clientIp);
}