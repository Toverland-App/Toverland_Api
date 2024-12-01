namespace Toverland_Api.Models
{
    public class Attraction
    {
        public int Id { get; set; }
        public required string Name { get; set; } // Add required modifier
        public int AreaId { get; set; }
        public required Area Area { get; set; } // Add required modifier
        public List<Maintenance> Maintenances { get; set; } = new List<Maintenance>(); // Initialize list
    }
}
