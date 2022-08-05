using Jordan.UrlShortener.Domain.Models;
using Jordan.UrlShortener.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Jordan.UrlShortener.Infrastructure.LocalDb.Context
{
    public class LocalDbContext : DbContext, IDbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> contextOptions) : base(contextOptions)
        {
            Database.EnsureCreated();
        }
        
        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        public async Task<int> Save() => 
            await base.SaveChangesAsync();
    }
}
