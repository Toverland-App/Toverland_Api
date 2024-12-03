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
        }

        public void Seed()
        {
            _logger.LogInformation("Starting database seeding...");

            try
            {
                if (!Areas.Any())
                {
                    _logger.LogInformation("Seeding Areas...");
                    Areas.AddRange(
                        new Area { Name = "Land van Toos" },
                        new Area { Name = "Avalon" },
                        new Area { Name = "Port Laguna" },
                        new Area { Name = "Ithaka" },
                        new Area { Name = "Magische Vallei" },
                        new Area { Name = "Wunderwald" }
                    );
                    SaveChanges();
                    _logger.LogInformation("Areas seeded.");
                }
                else
                {
                    _logger.LogInformation("Areas already exist. Skipping seeding.");
                }

                if (!Attractions.Any())
                {
                    _logger.LogInformation("Seeding Attractions...");
                    Attractions.AddRange(
               new Attraction { Name = "Fēnix" },
                        new Attraction { Name = "Troy" },
                        new Attraction { Name = "Dwervelwind" },
                        new Attraction { Name = "Merlin's Quest" },
                        new Attraction { Name = "Maximus' Blitz Bahn" },
                        new Attraction { Name = "Toos-Express" },
                        new Attraction { Name = "Djengu River" },
                        new Attraction { Name = "Scorpios" },
                        new Attraction { Name = "Booster Bike" },
                        new Attraction { Name = "Villa Fiasko" },
                        new Attraction { Name = "Expedition Zork" },
                        new Attraction { Name = "Magic Bikes" },
                        new Attraction { Name = "Tolly Molly" },
                        new Attraction { Name = "Paarden van Ithaka" }
                    );
                    SaveChanges();
                    _logger.LogInformation("Attractions seeded.");
                }
                else
                {
                    _logger.LogInformation("Attractions already exist. Skipping seeding.");
                }

                _logger.LogInformation("Database seeding completed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }
    }
}
