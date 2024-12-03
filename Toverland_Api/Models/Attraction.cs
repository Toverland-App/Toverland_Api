using System.Text.Json.Serialization;


namespace Toverland_Api.Models
{
    public class Attraction
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int AreaId { get; set; }

        [JsonIgnore] // Exclude Area from serialization
        public Area? Area { get; set; }
    } 

}
