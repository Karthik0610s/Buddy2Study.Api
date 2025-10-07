using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buddy2Study.Application.Dtos
{
    public  class InstitutionDto
    {
        public readonly string? ErrorMessage;

        // Institution Fields
        public int InstitutionID { get; set; } = 0;
        public string InstitutionName { get; set; } = string.Empty;
        public string InstitutionType { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public int NumStudentsEligible { get; set; } = 0;
        public string VerificationAuthority { get; set; } = string.Empty;
        public string InstitutionCreatedBy { get; set; } = string.Empty;
        public DateTime InstitutionCreatedDate { get; set; } = DateTime.UtcNow;
        public string InstitutionModifiedBy { get; set; } = string.Empty;
        public DateTime InstitutionModifiedDate { get; set; } = DateTime.UtcNow;
        // User Fields (can be null)
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int? RoleId { get; set; } = null;
        public string UserCreatedBy { get; set; } = string.Empty;
        public DateTime? UserCreatedDate { get; set; } = null;
    }
}
