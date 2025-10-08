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
        public string UserName { get; set; }
        public string PasswordHash { get; set; }   
        public string RoleId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? RoleName { get; set; }
        public string? Name { get; set; }
        public int UserId { get; set; }
    }

}
