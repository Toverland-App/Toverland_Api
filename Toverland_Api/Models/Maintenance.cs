namespace Toverland_Api.Models
{
    public class Maintenance
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public string? Status { get; set; }
        //public int AttractionId { get; set; }
        //public Attraction? Attraction { get; set; }

    }
}
