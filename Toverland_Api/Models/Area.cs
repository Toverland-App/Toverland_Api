using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Toverland_Api.Models
{
    public class AttractionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? MinHeight { get; set; }
        public string? Description { get; set; }
        public TimeSpan? OpeningTime { get; set; }
        public TimeSpan? ClosingTime { get; set; }
        public int? Capacity { get; set; }
        public double? QueueSpeed { get; set; }
        public int? QueueLength { get; set; }
        public string? Image { get; set; } // Added Image property
    }

    public class AreaWithAttractionsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Size { get; set; }
        public List<AttractionDto> Attractions { get; set; } = new List<AttractionDto>();
    }


    public class Area
    {
        private int _id;
        private string _name;
        private double _size;
        private List<Attraction> _attractions = new List<Attraction>();

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        [Required]
        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(Name));
        }

        public double Size
        {
            get => _size;
            set => _size = value;
        }

        [JsonIgnore]
        public List<Attraction> Attractions
        {
            get => _attractions;
            set => _attractions = value ?? new List<Attraction>();
        }
    }
}