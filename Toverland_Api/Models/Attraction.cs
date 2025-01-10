using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Toverland_Api.Models
{
    public class Attraction
    {
        private int _id;
        private string _name;
        private double? _minHeight;
        private int _areaId;
        private Area? _area;
        private string? _description;
        private TimeSpan? _openingTime;
        private TimeSpan? _closingTime;
        private int? _capacity;
        private double? _queueSpeed;
        private int? _queueLength;
        private object? _maintenances;

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

        public double? MinHeight
        {
            get => _minHeight;
            set => _minHeight = value;
        }

        [Required]
        public int AreaId
        {
            get => _areaId;
            set => _areaId = value;
        }

        [ForeignKey("AreaId")]
        [JsonIgnore]
        public Area? Area
        {
            get => _area;
            set => _area = value;
        }

        public string? Description
        {
            get => _description;
            set => _description = value;
        }

        public TimeSpan? OpeningTime
        {
            get => _openingTime;
            set => _openingTime = value;
        }

        public TimeSpan? ClosingTime
        {
            get => _closingTime;
            set => _closingTime = value;
        }

        public int? Capacity
        {
            get => _capacity;
            set => _capacity = value;
        }

        public double? QueueSpeed
        {
            get => _queueSpeed;
            set => _queueSpeed = value;
        }

        public int? QueueLength
        {
            get => _queueLength;
            set => _queueLength = value;
        }

        [NotMapped]
        public object? Maintenances
        {
            get => _maintenances;
            set => _maintenances = value;
        }
    }
}

