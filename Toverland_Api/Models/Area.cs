using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Toverland_Api.Models
{
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


