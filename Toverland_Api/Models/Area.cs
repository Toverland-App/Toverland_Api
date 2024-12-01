namespace Toverland_Api.Models
{
    public class Area
    {
        public int Id { get; set; }
        public required string Name { get; set; } // Add required modifier
        public List<Attraction> Attractions { get; set; } = new List<Attraction>(); // Initialize list
    }
}
