using System.Text.Json.Serialization;

namespace Toverland_Api.Models
{
    public class Maintenance
    {
        private int _id;
        private string? _description;
        private DateTime? _date;
        private string? _status;
        private int _attractionId;
        private Attraction? _attraction;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string? Description
        {
            get => _description;
            set => _description = value;
        }

        public DateTime? Date
        {
            get => _date;
            set => _date = value;
        }

        public string? Status
        {
            get => _status;
            set => _status = value;
        }

        public int AttractionId
        {
            get => _attractionId;
            set => _attractionId = value;
        }

        [JsonIgnore]
        public Attraction? Attraction
        {
            get => _attraction;
            set => _attraction = value;
        }
    }
}


