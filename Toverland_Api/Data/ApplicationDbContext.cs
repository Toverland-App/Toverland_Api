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
        public DbSet<Employee> Employees { get; set; }
        public DbSet<VisitorCount> VisitorCounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Area>()
                .HasMany(a => a.Attractions)
                .WithOne(at => at.Area)
                .HasForeignKey(at => at.AreaId);
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

                if (!Employees.Any())
                {
                    _logger.LogInformation("Seeding Employees...");
                    var areas = Areas.ToList(); // Fetch areas from the database
                    var employees = new[]
                    {
                            new Employee { Name = "John Doe", Role = "Manager", Area = areas[0], Email = "john.doe@example.com", PhoneNumber = "123-456-7890", HireDate = DateTime.Now.AddYears(-5), IsActive = true },
                            new Employee { Name = "Liam Smith", Role = "Operator", Area = areas[0], Email = "liam.smith@example.com", PhoneNumber = "234-567-8901", HireDate = DateTime.Now.AddYears(-3), IsActive = true },
                            new Employee { Name = "Sophia Brown", Role = "Technician", Area = areas[0], Email = "sophia.brown@example.com", PhoneNumber = "345-678-9012", HireDate = DateTime.Now.AddYears(-2), IsActive = true },
                            new Employee { Name = "James Johnson", Role = "Security", Area = areas[0], Email = "james.johnson@example.com", PhoneNumber = "456-789-0123", HireDate = DateTime.Now.AddYears(-4), IsActive = true },
                            new Employee { Name = "Emma Davis", Role = "Cleaner", Area = areas[0], Email = "emma.davis@example.com", PhoneNumber = "567-890-1234", HireDate = DateTime.Now.AddYears(-1), IsActive = true },
                            new Employee { Name = "Olivia White", Role = "Technician", Area = areas[0], Email = "olivia.white@example.com", PhoneNumber = "678-901-2345", HireDate = DateTime.Now.AddYears(-6), IsActive = true },
                            new Employee { Name = "Lucas Martinez", Role = "Supervisor", Area = areas[0], Email = "lucas.martinez@example.com", PhoneNumber = "789-012-3456", HireDate = DateTime.Now.AddYears(-3), IsActive = true },
                            new Employee { Name = "Isabella Wilson", Role = "Operator", Area = areas[0], Email = "isabella.wilson@example.com", PhoneNumber = "890-123-4567", HireDate = DateTime.Now.AddYears(-4), IsActive = true },
                            new Employee { Name = "Ethan Brown", Role = "Manager", Area = areas[0], Email = "ethan.brown@example.com", PhoneNumber = "901-234-5678", HireDate = DateTime.Now.AddYears(-5), IsActive = true },
                            new Employee { Name = "Mia Walker", Role = "Cleaner", Area = areas[0], Email = "mia.walker@example.com", PhoneNumber = "012-345-6789", HireDate = DateTime.Now.AddYears(-1), IsActive = true },

                            // Area 1
                            new Employee { Name = "Noah Taylor", Role = "Technician", Area = areas[1], Email = "noah.taylor@example.com", PhoneNumber = "123-555-7890", HireDate = DateTime.Now.AddYears(-5), IsActive = true },
                            new Employee { Name = "Emma Harris", Role = "Supervisor", Area = areas[1], Email = "emma.harris@example.com", PhoneNumber = "234-666-8901", HireDate = DateTime.Now.AddYears(-7), IsActive = true },
                            new Employee { Name = "Ava Lewis", Role = "Operator", Area = areas[1], Email = "ava.lewis@example.com", PhoneNumber = "345-777-9012", HireDate = DateTime.Now.AddYears(-3), IsActive = true },
                            new Employee { Name = "Charlotte Clark", Role = "Security", Area = areas[1], Email = "charlotte.clark@example.com", PhoneNumber = "456-888-0123", HireDate = DateTime.Now.AddYears(-6), IsActive = true },
                            new Employee { Name = "Mason Lee", Role = "Pizza", Area = areas[1], Email = "mason.lee@example.com", PhoneNumber = "567-999-1234", HireDate = DateTime.Now.AddYears(-2), IsActive = true },
                            new Employee { Name = "Amelia Hall", Role = "Cleaner", Area = areas[1], Email = "amelia.hall@example.com", PhoneNumber = "678-444-5678", HireDate = DateTime.Now.AddYears(-1), IsActive = true },
                            new Employee { Name = "Logan Allen", Role = "Technician", Area = areas[1], Email = "logan.allen@example.com", PhoneNumber = "890-555-6789", HireDate = DateTime.Now.AddYears(-4), IsActive = true },
                            new Employee { Name = "Harper Wright", Role = "Security", Area = areas[1], Email = "harper.wright@example.com", PhoneNumber = "901-666-3456", HireDate = DateTime.Now.AddYears(-5), IsActive = true },
                            new Employee { Name = "Ella King", Role = "Supervisor", Area = areas[1], Email = "ella.king@example.com", PhoneNumber = "234-777-8901", HireDate = DateTime.Now.AddYears(-3), IsActive = true },
                            new Employee { Name = "Jackson Lopez", Role = "Operator", Area = areas[1], Email = "jackson.lopez@example.com", PhoneNumber = "345-888-1234", HireDate = DateTime.Now.AddYears(-2), IsActive = true },

                            // Area 2
                            new Employee { Name = "Evelyn Scott", Role = "Technician", Area = areas[2], Email = "evelyn.scott@example.com", PhoneNumber = "123-123-4567", HireDate = DateTime.Now.AddYears(-3), IsActive = true },
                            new Employee { Name = "Alexander Perez", Role = "Cleaner", Area = areas[2], Email = "alexander.perez@example.com", PhoneNumber = "234-234-5678", HireDate = DateTime.Now.AddYears(-2), IsActive = true },
                            new Employee { Name = "Sophia Anderson", Role = "Operator", Area = areas[2], Email = "sophia.anderson@example.com", PhoneNumber = "345-345-6789", HireDate = DateTime.Now.AddYears(-1), IsActive = true },
                            new Employee { Name = "Michael Young", Role = "Security", Area = areas[2], Email = "michael.young@example.com", PhoneNumber = "456-456-7890", HireDate = DateTime.Now.AddYears(-4), IsActive = true },
                            new Employee { Name = "Isabella Thompson", Role = "Supervisor", Area = areas[2], Email = "isabella.thompson@example.com", PhoneNumber = "567-567-8901", HireDate = DateTime.Now.AddYears(-6), IsActive = true },
                            new Employee { Name = "Benjamin Rivera", Role = "Technician", Area = areas[2], Email = "benjamin.rivera@example.com", PhoneNumber = "678-678-9012", HireDate = DateTime.Now.AddYears(-3), IsActive = true },
                            new Employee { Name = "Abigail Hernandez", Role = "Cleaner", Area = areas[2], Email = "abigail.hernandez@example.com", PhoneNumber = "789-789-0123", HireDate = DateTime.Now.AddYears(-5), IsActive = true },
                            new Employee { Name = "Lucas Campbell", Role = "Manager", Area = areas[2], Email = "lucas.campbell@example.com", PhoneNumber = "890-890-1234", HireDate = DateTime.Now.AddYears(-7), IsActive = true },
                            new Employee { Name = "Chloe Stewart", Role = "Pizza", Area = areas[2], Email = "chloe.stewart@example.com", PhoneNumber = "901-901-2345", HireDate = DateTime.Now.AddYears(-3), IsActive = true },
                            new Employee { Name = "Ella Flores", Role = "Security", Area = areas[2], Email = "ella.flores@example.com", PhoneNumber = "123-123-3456", HireDate = DateTime.Now.AddYears(-6), IsActive = true },
                    
                            // Area 3
                            new Employee { Name = "Oliver Martin", Role = "Cleaner", Area = areas[3], Email = "oliver.martin@example.com", PhoneNumber = "234-234-4567", HireDate = DateTime.Now.AddYears(-3), IsActive = true },
                            new Employee { Name = "Liam Peterson", Role = "Technician", Area = areas[3], Email = "liam.peterson@example.com", PhoneNumber = "345-345-5678", HireDate = DateTime.Now.AddYears(-2), IsActive = true },
                            new Employee { Name = "Ava Roberts", Role = "Supervisor", Area = areas[3], Email = "ava.roberts@example.com", PhoneNumber = "456-456-7890", HireDate = DateTime.Now.AddYears(-4), IsActive = true },
                            new Employee { Name = "Ethan Wood", Role = "Security", Area = areas[3], Email = "ethan.wood@example.com", PhoneNumber = "567-567-8901", HireDate = DateTime.Now.AddYears(-1), IsActive = true },
                            new Employee { Name = "Amelia Fisher", Role = "Technician", Area = areas[3], Email = "amelia.fisher@example.com", PhoneNumber = "678-678-9012", HireDate = DateTime.Now.AddYears(-5), IsActive = true },
                            new Employee { Name = "Charlotte Bennett", Role = "Operator", Area = areas[3], Email = "charlotte.bennett@example.com", PhoneNumber = "789-789-0123", HireDate = DateTime.Now.AddYears(-2), IsActive = true },
                            new Employee { Name = "Mason Bell", Role = "Manager", Area = areas[3], Email = "mason.bell@example.com", PhoneNumber = "890-890-1234", HireDate = DateTime.Now.AddYears(-6), IsActive = true },
                            new Employee { Name = "Sophia Brooks", Role = "Cleaner", Area = areas[3], Email = "sophia.brooks@example.com", PhoneNumber = "901-901-2345", HireDate = DateTime.Now.AddYears(-7), IsActive = true },
                            new Employee { Name = "Isabella Jenkins", Role = "Pizza", Area = areas[3], Email = "isabella.jenkins@example.com", PhoneNumber = "123-123-3456", HireDate = DateTime.Now.AddYears(-3), IsActive = true },
                            new Employee { Name = "Logan Stewart", Role = "Technician", Area = areas[3], Email = "logan.stewart@example.com", PhoneNumber = "234-234-5678", HireDate = DateTime.Now.AddYears(-5), IsActive = true },

                             // Area 4
                            new Employee { Name = "Sophia Edwards", Role = "Pizza", Area = areas[4], Email = "sophia.edwards@example.com", PhoneNumber = "456-456-6789", HireDate = DateTime.Now.AddYears(-1), IsActive = true },
                            new Employee { Name = "Benjamin Morris", Role = "Cleaner", Area = areas[4], Email = "benjamin.morris@example.com", PhoneNumber = "567-567-7890", HireDate = DateTime.Now.AddYears(-3), IsActive = true },
                            new Employee { Name = "Ella Ramirez", Role = "Supervisor", Area = areas[4], Email = "ella.ramirez@example.com", PhoneNumber = "678-678-8901", HireDate = DateTime.Now.AddYears(-4), IsActive = true },
                            new Employee { Name = "Alexander Perry", Role = "Operator", Area = areas[4], Email = "alexander.perry@example.com", PhoneNumber = "789-789-9012", HireDate = DateTime.Now.AddYears(-5), IsActive = true },
                            new Employee { Name = "Ava Reed", Role = "Technician", Area = areas[4], Email = "ava.reed@example.com", PhoneNumber = "890-890-0123", HireDate = DateTime.Now.AddYears(-6), IsActive = true },
                            new Employee { Name = "Liam Carter", Role = "Security", Area = areas[4], Email = "liam.carter@example.com", PhoneNumber = "901-901-1234", HireDate = DateTime.Now.AddYears(-7), IsActive = true },
                            new Employee { Name = "Emma Foster", Role = "Manager", Area = areas[4], Email = "emma.foster@example.com", PhoneNumber = "123-123-2345", HireDate = DateTime.Now.AddYears(-8), IsActive = true },
                            new Employee { Name = "Noah Powell", Role = "Technician", Area = areas[4], Email = "noah.powell@example.com", PhoneNumber = "234-234-3456", HireDate = DateTime.Now.AddYears(-2), IsActive = true },
                            new Employee { Name = "Olivia Simmons", Role = "Cleaner", Area = areas[4], Email = "olivia.simmons@example.com", PhoneNumber = "345-345-4567", HireDate = DateTime.Now.AddYears(-5), IsActive = true },
                            new Employee { Name = "Evelyn Hayes", Role = "Supervisor", Area = areas[4], Email = "evelyn.hayes@example.com", PhoneNumber = "456-456-5678", HireDate = DateTime.Now.AddYears(-4), IsActive = true },
                    };

                    Employees.AddRange(employees);
                    SaveChanges();
                    _logger.LogInformation("Employees seeded.");
                }
                else
                {
                    _logger.LogInformation("Employees already exist. Skipping seeding.");
                }

                if (!VisitorCounts.Any())
                {
                    _logger.LogInformation("Seeding VisitorCounts...");
                    var visitorCounts = new[]
                    {
                       new VisitorCount { Date = DateTime.Today, Count = 100 },
                       new VisitorCount { Date = DateTime.Today.AddDays(-1), Count = 150 }
                   };
                    VisitorCounts.AddRange(visitorCounts);
                    SaveChanges();
                    _logger.LogInformation("VisitorCounts seeded.");
                }
                else
                {
                    _logger.LogInformation("VisitorCounts already exist. Skipping seeding.");
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
