using Jordan.UrlShortener.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Jordan.UrlShortener.Infrastructure.Context
{
    public interface IDbContext
    {
        DbSet<ShortenedUrl> ShortenedUrls { get; set; }
        Task<int> Save();
    }
}
