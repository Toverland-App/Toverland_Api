using System.Text.Json.Serialization;

namespace Toverland_Api.Models
{
    public class Maintenance
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public string? Status { get; set; }

        // Foreign key to Attraction
        public int AttractionId { get; set; } // Fixed missing closing brace
        [JsonIgnore]
        public Attraction? Attraction { get; set; }
    }
}
