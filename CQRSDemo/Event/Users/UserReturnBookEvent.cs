using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Users
{
    public class UserReturnBookEvent : EventBase
    {
        public Guid UserAggregateRootId { get; set; }
        public Guid BookAggregateRootId { get; set; }
        public DateTime ReturnedDate { get; set; }
    }
}
