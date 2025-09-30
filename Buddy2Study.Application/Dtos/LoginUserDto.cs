using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buddy2Study.Application.Dtos
{
    public class LoginUserDto
    {
        public string Username { get; set; }   // required for login
        public string Password { get; set; }   // plain password from client
    }
}
