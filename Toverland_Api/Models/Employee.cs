namespace Toverland_Api.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Role { get; set; }
        public int AreaId { get; set; }
        public Area? Area { get; set; }
    }
}