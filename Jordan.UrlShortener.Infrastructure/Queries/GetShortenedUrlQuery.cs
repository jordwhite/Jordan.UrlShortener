using Jordan.UrlShortener.Application.Queries;
using Jordan.UrlShortener.Domain.Models;
using Jordan.UrlShortener.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Jordan.UrlShortener.Infrastructure.Queries
{
    public class GetShortenedUrlQuery : IGetShortenedUrlQuery
    {
        private readonly IDbContext _dbContext;

        public GetShortenedUrlQuery(IDbContext dbContext) =>
            _dbContext = dbContext;

        public Task<ShortenedUrl> GetByFullUrl(string fullUrl) => 
            _dbContext.ShortenedUrls.FirstOrDefaultAsync(
                url => url.FullUrl.ToUpper() ==  fullUrl.ToUpper()
            );

        public Task<ShortenedUrl> GetById(string id) => 
            _dbContext.ShortenedUrls.FirstOrDefaultAsync(
                url => url.Id.ToUpper() == id.ToUpper()
            );
    }
}
