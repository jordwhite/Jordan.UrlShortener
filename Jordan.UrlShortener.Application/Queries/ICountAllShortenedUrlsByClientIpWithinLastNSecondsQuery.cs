namespace Jordan.UrlShortener.Application.Queries
{
    public interface ICountAllShortenedUrlsByClientIpWithinLastNSecondsQuery
    {
        public Task<int> CountAll(string clientIp, double seconds);
    }
}
