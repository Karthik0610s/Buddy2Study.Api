using System;

namespace Buddy2Study.Domain.Entities
{
    public class Sponsors
    {
        public int Id { get; set; }

        // Core fields (required for insert)
        public string OrganizationName { get; set; }
        public string OrganizationType { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }

        // Optional UI fields (NOT required for insert)
        public string? ContactPerson { get; set; }
        public string? Address { get; set; }
        public decimal? Budget { get; set; }   // use decimal? for numeric budget
        public string? StudentCriteria { get; set; }
        public string? StudyLevels { get; set; }

        // Insert-related
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public int? RoleId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        // Update-related
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
