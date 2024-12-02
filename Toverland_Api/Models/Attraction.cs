namespace Toverland_Api.Models
{
    public class Attraction
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        //public int AreaId { get; set; }
        //public Area? Area { get; set; }
        //public List<Maintenance>? Maintenances { get; set; } = new();
    }
}
