using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Toverland_Api.Models
{
    public class Employee
    {
        private int _id;
        private string? _name;
        private string? _role;
        private int? _areaId;
        private Area? _area;
        private string? _email;
        private string? _phoneNumber;
        private DateTime? _hireDate;
        private bool? _isActive;
        private List<Attraction>? _attractions;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string? Name
        {
            get => _name;
            set => _name = value;
        }

        public string? Role
        {
            get => _role;
            set => _role = value;
        }

        public int? AreaId
        {
            get => _areaId;
            set => _areaId = value;
        }

        [JsonIgnore]
        public Area? Area
        {
            get => _area;
            set => _area = value;
        }

        public string? Email
        {
            get => _email;
            set => _email = value;
        }

        public string? PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        public DateTime? HireDate
        {
            get => _hireDate;
            set => _hireDate = value;
        }

        public bool? IsActive
        {
            get => _isActive;
            set => _isActive = value;
        }

        [JsonIgnore]
        public List<Attraction>? Attractions
        {
            get => _attractions;
            set => _attractions = value;
        }
    }
}

