namespace Toverland_Api.Models
{
    public class Feedback
    {
        private int _id;
        private int _visitorId;
        private Visitor? _visitor;
        private string? _description;
        private string? _date;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public int VisitorId
        {
            get => _visitorId;
            set => _visitorId = value;
        }

        public Visitor? Visitor
        {
            get => _visitor;
            set => _visitor = value;
        }

        public string? Description
        {
            get => _description;
            set => _description = value;
        }

        public string? Date
        {
            get => _date;
            set => _date = value;
        }
    }
}


