namespace Jordan.UrlShortener.Application.Services;

public interface IThrottleService
{
    Task<bool> ShouldThrottleClientIp(string clientIp);
}