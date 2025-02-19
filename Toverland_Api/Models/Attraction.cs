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

        // none parameter constructor is default
        public Attraction() { }

        // all parameters given to constructor
        public Attraction(int id, string name, double? minHeight, int areaId, string? description, TimeSpan? openingTime, TimeSpan? closingTime, int? capacity, double? queueSpeed, int? queueLength)
        {
            _id = id;
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _minHeight = minHeight;
            _areaId = areaId == 0 ? 1 : areaId;
            _description = description;
            _openingTime = openingTime ?? new TimeSpan(9, 0, 0);
            _closingTime = closingTime ?? new TimeSpan(18, 0, 0);
            _capacity = capacity;
            _queueSpeed = queueSpeed;
            _queueLength = queueLength;
        }

        // constructor without id
        public Attraction(string name, double? minHeight, int areaId, string? description, TimeSpan? openingTime, TimeSpan? closingTime, int? capacity, double? queueSpeed, int? queueLength)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _minHeight = minHeight;
            _areaId = areaId == 0 ? 1 : areaId;
            _description = description;
            _openingTime = openingTime ?? new TimeSpan(9, 0, 0);
            _closingTime = closingTime ?? new TimeSpan(18, 0, 0);
            _capacity = capacity;
            _queueSpeed = queueSpeed;
            _queueLength = queueLength;
        }

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
            set => _areaId = value == 0 ? 1 : value;
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
            set => _openingTime = value ?? new TimeSpan(9, 0, 0);
        }

        public TimeSpan? ClosingTime
        {
            get => _closingTime;
            set => _closingTime = value ?? new TimeSpan(18, 0, 0);
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
