using System;

namespace Buddy2Study.Domain.Entities
{
    public class Sponsors
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationType { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }

        // Added properties for SP
        public string Username { get; set; }          // maps to SP Username
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public string ModifiedBy { get; set; }        // for update
        public DateTime? ModifiedDate { get; set; }   // optional
    }
}
