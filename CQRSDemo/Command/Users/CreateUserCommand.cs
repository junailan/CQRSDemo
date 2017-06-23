using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Users
{
    public class CreateUserCommand : CommandBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string ContactPhone { get; set; }
        public string Address_Country { get; set; }
        public string Address_State { get; set; }
        public string Address_Street { get; set; }
        public string Address_City { get; set; }
        public string Address_Zip { get; set; }
    }
}
