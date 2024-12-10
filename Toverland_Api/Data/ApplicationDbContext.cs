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
                    var areas = new[]
                    {
                        new Area { Name = "Land van Toos", Size = 10.5 },
                        new Area { Name = "Avalon", Size = 15.0 },
                        new Area { Name = "Port Laguna", Size = 8.3 },
                        new Area { Name = "Ithaka", Size = 12.7 },
                        new Area { Name = "Magische Vallei", Size = 14.2 },
                        new Area { Name = "Wunderwald", Size = 9.8 }
                    };
                    Areas.AddRange(areas);
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
                    var areas = Areas.ToList(); // Fetch areas from the database
                    var attractions = new[]
                    {
                        new Attraction { Name = "Fēnix", Area = areas[1], MinHeight = 1.4, Description = "A thrilling roller coaster", OpeningTime = new TimeSpan(9, 0, 0), ClosingTime = new TimeSpan(18, 0, 0), Capacity = 1000, QueueSpeed = 20, QueueLength = 50 }, // Avalon
                        new Attraction { Name = "Troy", Area = areas[3], MinHeight = 1.2, Description = "A wooden roller coaster", OpeningTime = new TimeSpan(9, 0, 0), ClosingTime = new TimeSpan(18, 0, 0), Capacity = 800, QueueSpeed = 15, QueueLength = 40 }, // Ithaka
                        new Attraction { Name = "Dwervelwind", Area = areas[4], MinHeight = 1.3, Description = "A spinning coaster", OpeningTime = new TimeSpan(9, 0, 0), ClosingTime = new TimeSpan(18, 0, 0), Capacity = 600, QueueSpeed = 10, QueueLength = 30 }, // Magische Vallei
                        new Attraction { Name = "Merlin's Quest", Area = areas[1], MinHeight = 1.0, Description = "A boat ride", OpeningTime = new TimeSpan(9, 0, 0), ClosingTime = new TimeSpan(18, 0, 0), Capacity = 500, QueueSpeed = 8, QueueLength = 20 }, // Avalon
                        new Attraction { Name = "Maximus' Blitz Bahn", Area = areas[0], MinHeight = 1.1, Description = "A bobsled coaster", OpeningTime = new TimeSpan(9, 0, 0), ClosingTime = new TimeSpan(18, 0, 0), Capacity = 700, QueueSpeed = 12, QueueLength = 25 }, // Land van Toos
                        new Attraction { Name = "Toos-Express", Area = areas[0], MinHeight = 1.0, Description = "A family coaster", OpeningTime = new TimeSpan(9, 0, 0), ClosingTime = new TimeSpan(18, 0, 0), Capacity = 900, QueueSpeed = 18, QueueLength = 35 }, // Land van Toos
                        new Attraction { Name = "Djengu River", Area = areas[4], MinHeight = 1.2, Description = "A river rapids ride", OpeningTime = new TimeSpan(9, 0, 0), ClosingTime = new TimeSpan(18, 0, 0), Capacity = 600, QueueSpeed = 10, QueueLength = 30 }, // Magische Vallei
                        new Attraction { Name = "Scorpios", Area = areas[3], MinHeight = 1.1, Description = "A pirate ship ride", OpeningTime = new TimeSpan(9, 0, 0), ClosingTime = new TimeSpan(18, 0, 0), Capacity = 500, QueueSpeed = 8, QueueLength = 20 }, // Ithaka
                        new Attraction { Name = "Booster Bike", Area = areas[5], MinHeight = 1.3, Description = "A motorbike coaster", OpeningTime = new TimeSpan(9, 0, 0), ClosingTime = new TimeSpan(18, 0, 0), Capacity = 700, QueueSpeed = 12, QueueLength = 25 }, // Wunderwald
                        new Attraction { Name = "Villa Fiasko", Area = areas[0], MinHeight = 1.0, Description = "A fun house", OpeningTime = new TimeSpan(9, 0, 0), ClosingTime = new TimeSpan(18, 0, 0), Capacity = 400, QueueSpeed = 5, QueueLength = 15 }, // Land van Toos
                        new Attraction { Name = "Expedition Zork", Area = areas[4], MinHeight = 1.2, Description = "A log flume ride", OpeningTime = new TimeSpan(9, 0, 0), ClosingTime = new TimeSpan(18, 0, 0), Capacity = 600, QueueSpeed = 10, QueueLength = 30 }, // Magische Vallei
                        new Attraction { Name = "Magic Bikes", Area = areas[0], MinHeight = 1.0, Description = "A flying bike ride", OpeningTime = new TimeSpan(9, 0, 0), ClosingTime = new TimeSpan(18, 0, 0), Capacity = 500, QueueSpeed = 8, QueueLength = 20 }, // Land van Toos
                        new Attraction { Name = "Tolly Molly", Area = areas[2], MinHeight = 1.0, Description = "A carousel", OpeningTime = new TimeSpan(9, 0, 0), ClosingTime = new TimeSpan(18, 0, 0), Capacity = 300, QueueSpeed = 5, QueueLength = 10 }, // Port Laguna
                        new Attraction { Name = "Paarden van Ithaka", Area = areas[3], MinHeight = 1.0, Description = "A horse ride", OpeningTime = new TimeSpan(9, 0, 0), ClosingTime = new TimeSpan(18, 0, 0), Capacity = 400, QueueSpeed = 5, QueueLength = 15 } // Ithaka
                    };

                    Attractions.AddRange(attractions);
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
