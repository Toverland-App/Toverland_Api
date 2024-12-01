using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Toverland_Api.Models;
using System.Linq;

namespace Toverland_Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ILogger<ApplicationDbContext> _logger;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger)
            : base(options)
        {
            _logger = logger;
        }

        public DbSet<Area> Areas { get; set; }
        public DbSet<Attraction> Attractions { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-many relationship between Area and Attraction
            modelBuilder.Entity<Attraction>()
                .HasOne(a => a.Area)
                .WithMany(b => b.Attractions)
                .HasForeignKey(a => a.AreaId);

            // Configure one-to-many relationship between Attraction and Maintenance
            modelBuilder.Entity<Maintenance>()
                .HasOne(m => m.Attraction)
                .WithMany(a => a.Maintenances)
                .HasForeignKey(m => m.AttractionId);
        }

        public void Seed()
        {
            _logger.LogInformation("Starting database seeding...");

            if (!Areas.Any())
            {
                _logger.LogInformation("Seeding Areas...");
                Areas.AddRange(
                    new Area { Name = "Fantasy" },
                    new Area { Name = "Adventure" },
                    new Area { Name = "Magic" }
                );
                SaveChanges();
                _logger.LogInformation("Areas seeded.");
            }
            else
            {
                _logger.LogInformation("Areas already exist. Skipping seeding.");
            }

            _logger.LogInformation("Database seeding completed.");
        }
    }
}
