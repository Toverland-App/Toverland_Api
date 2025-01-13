namespace Toverland_Api.Models
{
    public class Feedback
    {
        private int _id;
        private string? _description;
        private string? _date;
        private int? _rating;

        public Feedback() { }

        public Feedback(int id, string? description, string? date, int? rating)
        {
            _id = id;
            _description = description;
            _date = string.IsNullOrEmpty(date) || date == "string" ? DateTime.Now.ToString("yyyy-MM-dd") : date;
            Rating = rating; // Use the property setter to apply validation
        }

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

        public string? Date
        {
            get => _date;
            set => _date = string.IsNullOrEmpty(value) || value == "string" ? DateTime.Now.ToString("yyyy-MM-dd") : value;
        }

        public int? Rating
        {
            get => _rating;
            set
            {
                if (value != null && value != 0 && value != 1 && value != 2)
                {
                    throw new ArgumentOutOfRangeException(nameof(Rating), "Rating can only be 0, 1, or 2.");
                }
                _rating = value;
            }
        }
    }
}

