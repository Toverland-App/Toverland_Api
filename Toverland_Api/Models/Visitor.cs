namespace Toverland_Api.Models
{
    public class Visitor
    {
        public int Id { get; set; }
        public required string Name { get; set; } // Add required modifier
        public required List<Feedback> Feedbacks { get; set; } // Add required modifier
    }
}
