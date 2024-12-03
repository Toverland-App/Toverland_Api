namespace Toverland_Api.Models
{
    public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Size { get; set; }

        public List<Attraction>? Attractions { get; set; } = new();
    }
}
