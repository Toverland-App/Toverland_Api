﻿namespace Toverland_Api.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Role { get; set; }
        public int? AreaId { get; set; }
        public Area? Area { get; set; }

        // Additional properties
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? HireDate { get; set; }
        public bool? IsActive { get; set; }

        // Navigation property for attractions
        public List<Attraction>? Attractions { get; set; }
    }
}
