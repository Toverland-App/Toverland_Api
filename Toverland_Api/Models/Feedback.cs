namespace Toverland_Api.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public int VisitorId { get; set; }
        public Visitor? Visitor { get; set; }
        public string? Description { get; set; }
        public string? Date { get; set; }
    }
}