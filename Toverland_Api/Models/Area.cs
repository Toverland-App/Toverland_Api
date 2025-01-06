using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Toverland_Api.Models
{
    public class Area
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double Size { get; set; }

        [JsonIgnore] // Exclude Area from serialization
        public List<Attraction> Attractions { get; set; } = new List<Attraction>();
    }
}
