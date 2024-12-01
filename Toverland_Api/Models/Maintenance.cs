namespace Toverland_Api.Models
{
    public class Maintenance
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int AttractionId { get; set; }
        public required Attraction Attraction { get; set; } // Add required modifier
    }
}
