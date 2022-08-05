using Jordan.UrlShortener.Application.Queries;
using Jordan.UrlShortener.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Jordan.UrlShortener.Infrastructure.Queries
{
    public class CountAllShortenedUrlsByClientIpWithinLastNSecondsQuery : ICountAllShortenedUrlsByClientIpWithinLastNSecondsQuery
    {
        private readonly IDbContext _dbContext;

        public CountAllShortenedUrlsByClientIpWithinLastNSecondsQuery(IDbContext dbContext) => 
            _dbContext = dbContext;

        public async Task<int> CountAll(string clientIp, double seconds)
        {
            var cutOff = DateTime.UtcNow - TimeSpan.FromSeconds(seconds);
            return await _dbContext.ShortenedUrls.CountAsync(url => url.CreatedOn >= cutOff);
        }
    }
}
