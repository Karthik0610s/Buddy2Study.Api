using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buddy2Study.Domain.Entities
{
    public class Users
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LatName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DateofBirth { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }   
        public string Gender { get; set; }
        public string Course { get; set; }   // must match entity
        public string College { get; set; }
        public string Year { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
