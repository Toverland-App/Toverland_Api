using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Toverland_Api.Models
{
    public class Attraction
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double? MinHeight { get; set; }

        [Required]
        public int AreaId { get; set; }

        [ForeignKey("AreaId")]
        [JsonIgnore] // Exclude Area from serialization
        public Area? Area { get; set; }

        public string? Description { get; set; }

        public TimeSpan? OpeningTime { get; set; }
        public TimeSpan? ClosingTime { get; set; }
        public int? Capacity { get; set; }
        public double? QueueSpeed { get; set; } // e.g., number of people per minute
        public int? QueueLength { get; set; }

        [NotMapped]
        public object? Maintenances { get; set; }
    }
}
