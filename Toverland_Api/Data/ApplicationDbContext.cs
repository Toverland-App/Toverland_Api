using Microsoft.EntityFrameworkCore;

namespace Toverland_Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet voor de artikelen
        public DbSet<Area> Area { get; set; }
    }
}
