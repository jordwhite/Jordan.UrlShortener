using Jordan.UrlShortener.Application.Repositories;
using Jordan.UrlShortener.Domain.Models;
using Jordan.UrlShortener.Infrastructure.Context;

namespace Jordan.UrlShortener.Infrastructure.Repositories
{
    public class ShortenedUrlRepository : IShortenedUrlRepository
    {
        private readonly IDbContext _dbContext;

        public ShortenedUrlRepository(IDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task Add(ShortenedUrl entity)
        {
            await _dbContext.ShortenedUrls.AddAsync(entity);
            await _dbContext.Save();
        }
    }
}
