using Microsoft.EntityFrameworkCore;
using MiMangaBot.Domain;

namespace MiMangaBot.Infrastructure.Database
{
    public class MangaDbContext : DbContext
    {
        public MangaDbContext(DbContextOptions<MangaDbContext> options)
            : base(options) { }

        public DbSet<Manga> Mangas { get; set; }
    }
}
