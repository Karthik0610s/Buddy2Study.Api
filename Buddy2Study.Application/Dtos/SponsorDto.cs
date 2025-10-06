using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Buddy2Study.Application.Dtos
{
    /// <summary>
    /// DTO for returning Sponsor + User info after insert
    /// </summary>
    public class SponsorDto
    {
        public int Id { get; set; }                  // SponsorId
        public string OrganizationName { get; set; }
        public string OrganizationType { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Username { get; set; }         
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
