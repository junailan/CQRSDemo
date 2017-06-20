using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Users
{
    public class UserUpdateEvent : EventBase
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string ContactPhone { get; set; }
        public string Address_Country { get; set; }
        public string Address_State { get; set; }
        public string Address_City { get; set; }
        public string Address_Street { get; set; }
        public string Address_Zip { get; set; }
    }
}
