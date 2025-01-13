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
        public DbSet<Feedback> Feedbacks { get; set; }

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
                        new Attraction(1, "Fēnix", 1.4, areas[1].Id, "A thrilling roller coaster", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1000, 20, 50), // Avalon
                        new Attraction(2, "Troy", 1.2, areas[3].Id, "A wooden roller coaster", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 800, 15, 40), // Ithaka
                        new Attraction(3, "Dwervelwind", 1.3, areas[4].Id, "A spinning coaster", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 600, 10, 30), // Magische Vallei
                        new Attraction(4, "Merlin's Quest", 1.0, areas[1].Id, "A boat ride", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 500, 8, 20), // Avalon
                        new Attraction(5, "Maximus' Blitz Bahn", 1.1, areas[0].Id, "A bobsled coaster", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 700, 12, 25), // Land van Toos
                        new Attraction(6, "Toos-Express", 1.0, areas[0].Id, "A family coaster", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 900, 18, 35), // Land van Toos
                        new Attraction(7, "Djengu River", 1.2, areas[4].Id, "A river rapids ride", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 600, 10, 30), // Magische Vallei
                        new Attraction(8, "Scorpios", 1.1, areas[3].Id, "A pirate ship ride", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 500, 8, 20), // Ithaka
                        new Attraction(9, "Booster Bike", 1.3, areas[5].Id, "A motorbike coaster", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 700, 12, 25), // Wunderwald
                        new Attraction(10, "Villa Fiasko", 1.0, areas[0].Id, "A fun house", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 400, 5, 15), // Land van Toos
                        new Attraction(11, "Expedition Zork", 1.2, areas[4].Id, "A log flume ride", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 600, 10, 30), // Magische Vallei
                        new Attraction(12, "Magic Bikes", 1.0, areas[0].Id, "A flying bike ride", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 500, 8, 20), // Land van Toos
                        new Attraction(13, "Tolly Molly", 1.0, areas[2].Id, "A carousel", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 300, 5, 10), // Port Laguna
                        new Attraction(14, "Paarden van Ithaka", 1.0, areas[3].Id, "A horse ride", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 400, 5, 15) // Ithaka
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
                            // Area 0
                            new Employee { Name = "John Doe", Role = "Manager", Area = areas[0], Email = "john.doe@example.com", PhoneNumber = "123-456-7890", HireDate = DateTime.Now.AddYears(-5), IsActive = true },
                            new Employee { Name = "Liam Smith", Role = "Operator", Area = areas[0], Email = "liam.smith@example.com", PhoneNumber = "234-567-8901", HireDate = DateTime.Now.AddYears(-3), IsActive = true },
                            new Employee { Name = "Sophia Brown", Role = "Technician", Area = areas[0], Email = "sophia.brown@example.com", PhoneNumber = "345-678-9012", HireDate = DateTime.Now.AddYears(-2), IsActive = true },
                            new Employee { Name = "James Johnson", Role = "Security", Area = areas[0], Email = "james.johnson@example.com", PhoneNumber = "456-789-0123", HireDate = DateTime.Now.AddYears(-4), IsActive = true },
                            new Employee { Name = "Emma Davis", Role = "Cleaner", Area = areas[0], Email = "emma.davis@example.com", PhoneNumber = "567-890-1234", HireDate = DateTime.Now.AddYears(-1), IsActive = true },
                            new Employee { Name = "Olivia White", Role = "Technician", Area = areas[0], Email = "olivia.white@example.com", PhoneNumber = "678-901-2345", HireDate = DateTime.Now.AddYears(-6), IsActive = true },
                            new Employee { Name = "Lucas Martinez", Role = "Supervisor", Area = areas[0], Email = "lucas.martinez@example.com", PhoneNumber = "789-012-3456", HireDate = DateTime.Now.AddYears(-3), IsActive = true },
                            new Employee { Name = "Isabella Wilson", Role = "Operator", Area = areas[0], Email = "isabella.wilson@example.com", PhoneNumber = "890-123-4567", HireDate = DateTime.Now.AddYears(-4), IsActive = true },
                            new Employee { Name = "Ethan Brown", Role = "Operator", Area = areas[0], Email = "ethan.brown@example.com", PhoneNumber = "901-234-5678", HireDate = DateTime.Now.AddYears(-5), IsActive = true },
                            new Employee { Name = "Mia Walker", Role = "Cleaner", Area = areas[0], Email = "mia.walker@example.com", PhoneNumber = "012-345-6789", HireDate = DateTime.Now.AddYears(-1), IsActive = true },

                            // Area 1
                            new Employee { Name = "Noah Taylor", Role = "Technician", Area = areas[1], Email = "noah.taylor@example.com", PhoneNumber = "123-555-7890", HireDate = DateTime.Now.AddYears(-5), IsActive = true },
                            new Employee { Name = "Emma Harris", Role = "Supervisor", Area = areas[1], Email = "emma.harris@example.com", PhoneNumber = "234-666-8901", HireDate = DateTime.Now.AddYears(-7), IsActive = true },
                            new Employee { Name = "Ava Lewis", Role = "Operator", Area = areas[1], Email = "ava.lewis@example.com", PhoneNumber = "345-777-9012", HireDate = DateTime.Now.AddYears(-3), IsActive = true },
                            new Employee { Name = "Charlotte Clark", Role = "Security", Area = areas[1], Email = "charlotte.clark@example.com", PhoneNumber = "456-888-0123", HireDate = DateTime.Now.AddYears(-6), IsActive = true },
                            new Employee { Name = "Mason Lee", Role = "Operator", Area = areas[1], Email = "mason.lee@example.com", PhoneNumber = "567-999-1234", HireDate = DateTime.Now.AddYears(-2), IsActive = true },
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
                            new Employee { Name = "Chloe Stewart", Role = "Operator", Area = areas[2], Email = "chloe.stewart@example.com", PhoneNumber = "901-901-2345", HireDate = DateTime.Now.AddYears(-3), IsActive = true },
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
                            new Employee { Name = "Isabella Jenkins", Role = "Operator", Area = areas[3], Email = "isabella.jenkins@example.com", PhoneNumber = "123-123-3456", HireDate = DateTime.Now.AddYears(-3), IsActive = true },
                            new Employee { Name = "Logan Stewart", Role = "Technician", Area = areas[3], Email = "logan.stewart@example.com", PhoneNumber = "234-234-5678", HireDate = DateTime.Now.AddYears(-5), IsActive = true },

                             // Area 4
                            new Employee { Name = "Sophia Edwards", Role = "Operator", Area = areas[4], Email = "sophia.edwards@example.com", PhoneNumber = "456-456-6789", HireDate = DateTime.Now.AddYears(-1), IsActive = true },
                            new Employee { Name = "Benjamin Morris", Role = "Cleaner", Area = areas[4], Email = "benjamin.morris@example.com", PhoneNumber = "567-567-7890", HireDate = DateTime.Now.AddYears(-3), IsActive = true },
                            new Employee { Name = "Ella Ramirez", Role = "Supervisor", Area = areas[4], Email = "ella.ramirez@example.com", PhoneNumber = "678-678-8901", HireDate = DateTime.Now.AddYears(-4), IsActive = true },
                            new Employee { Name = "Alexander Perry", Role = "Operator", Area = areas[4], Email = "alexander.perry@example.com", PhoneNumber = "789-789-9012", HireDate = DateTime.Now.AddYears(-5), IsActive = true },
                            new Employee { Name = "Ava Reed", Role = "Technician", Area = areas[4], Email = "ava.reed@example.com", PhoneNumber = "890-890-0123", HireDate = DateTime.Now.AddYears(-6), IsActive = true },
                            new Employee { Name = "Liam Carter", Role = "Security", Area = areas[4], Email = "liam.carter@example.com", PhoneNumber = "901-901-1234", HireDate = DateTime.Now.AddYears(-7), IsActive = true },
                            new Employee { Name = "Emma Foster", Role = "Manager", Area = areas[4], Email = "emma.foster@example.com", PhoneNumber = "123-123-2345", HireDate = DateTime.Now.AddYears(-8), IsActive = true },
                            new Employee { Name = "Noah Powell", Role = "Technician", Area = areas[4], Email = "noah.powell@example.com", PhoneNumber = "234-234-3456", HireDate = DateTime.Now.AddYears(-2), IsActive = true },
                            new Employee { Name = "Olivia Simmons", Role = "Cleaner", Area = areas[4], Email = "olivia.simmons@example.com", PhoneNumber = "345-345-4567", HireDate = DateTime.Now.AddYears(-5), IsActive = true },
                            new Employee { Name = "Evelyn Hayes", Role = "Supervisor", Area = areas[4], Email = "evelyn.hayes@example.com", PhoneNumber = "456-456-5678", HireDate = DateTime.Now.AddYears(-4), IsActive = true },
                    
                            // Area 5
                            new Employee { Name = "Jan de Vries", Role = "Manager", Area = areas[5], Email = "jan.devries@example.com", PhoneNumber = "123-555-7890", HireDate = DateTime.Now.AddYears(-7), IsActive = true },
                            new Employee { Name = "Emma Jansen", Role = "Technician", Area = areas[5], Email = "emma.jansen@example.com", PhoneNumber = "234-666-8901", HireDate = DateTime.Now.AddYears(-3), IsActive = true },
                            new Employee { Name = "Tom Smith", Role = "Operator", Area = areas[5], Email = "tom.smith@example.com", PhoneNumber = "345-777-9012", HireDate = DateTime.Now.AddYears(-5), IsActive = true },
                            new Employee { Name = "Sophie Brown", Role = "Cleaner", Area = areas[5], Email = "sophie.brown@example.com", PhoneNumber = "456-888-0123", HireDate = DateTime.Now.AddYears(-2), IsActive = true },
                            new Employee { Name = "Pieter Bakker", Role = "Security", Area = areas[5], Email = "pieter.bakker@example.com", PhoneNumber = "567-999-1234", HireDate = DateTime.Now.AddYears(-4), IsActive = true },
                            new Employee { Name = "Lisa Taylor", Role = "Supervisor", Area = areas[5], Email = "lisa.taylor@example.com", PhoneNumber = "678-444-2345", HireDate = DateTime.Now.AddYears(-1), IsActive = true },
                            new Employee { Name = "Mark Visser", Role = "Technician", Area = areas[5], Email = "mark.visser@example.com", PhoneNumber = "789-333-3456", HireDate = DateTime.Now.AddYears(-6), IsActive = true },
                            new Employee { Name = "Anne Wilson", Role = "Operator", Area = areas[5], Email = "anne.wilson@example.com", PhoneNumber = "890-222-4567", HireDate = DateTime.Now.AddYears(-3), IsActive = true },
                            new Employee { Name = "David Johnson", Role = "Operator", Area = areas[5], Email = "david.johnson@example.com", PhoneNumber = "901-111-5678", HireDate = DateTime.Now.AddYears(-8), IsActive = true },
                            new Employee { Name = "Kim de Jong", Role = "Cleaner", Area = areas[5], Email = "kim.dejong@example.com", PhoneNumber = "123-000-6789", HireDate = DateTime.Now.AddYears(-2), IsActive = true },

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
                if (!Feedbacks.Any())
                {
                    _logger.LogInformation("Seeding Feedbacks...");
                    var feedbacks = new[] {
                        new Feedback(1, "Great park!", "2021-09-01", 2),
                        new Feedback(2, "I had a lot of fun!", "2021-09-02", 1),
                        new Feedback(3, "The food was delicious!", "2021-09-03", 2),
                        new Feedback(4, "The staff was very friendly.", "2021-09-04", 2),
                        new Feedback(5, "The park was clean and well-maintained.", "2021-09-05", 2),
                        new Feedback(6, "I didn't like the long lines.", "2021-09-06", 0),
                        new Feedback(7, "The park was too crowded.", "2021-09-07", 0),
                        new Feedback(8, "The park was too expensive.", "2021-09-08", 0),
                        new Feedback(9, "I got lost in the park.", "2021-09-09", 1),
                        new Feedback(10, "I didn't like the food.", "2021-09-10", 0)
                    };
                    Feedbacks.AddRange(feedbacks);
                    SaveChanges();
                    _logger.LogInformation("Feedbacks seeded.");
                }
                else
                {
                    _logger.LogInformation("Feedbacks already exist. Skipping seeding.");
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
