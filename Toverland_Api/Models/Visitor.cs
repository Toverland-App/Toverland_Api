namespace Toverland_Api.Models
{
    public class Visitor
    {
        private int _id;
        private string _name;
        private List<Feedback> _feedbacks;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public required string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(Name));
        }

        public required List<Feedback> Feedbacks
        {
            get => _feedbacks;
            set => _feedbacks = value ?? throw new ArgumentNullException(nameof(Feedbacks));
        }
    }
}


