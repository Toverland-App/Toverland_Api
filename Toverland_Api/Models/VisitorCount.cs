namespace Toverland_Api.Models
{
    public class VisitorCount
    {
        private int _id;
        private DateTime? _date;
        private int? _count;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public DateTime? Date
        {
            get => _date;
            set => _date = value;
        }

        public int? Count
        {
            get => _count;
            set => _count = value;
        }
    }
}



