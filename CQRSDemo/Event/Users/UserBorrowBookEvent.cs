using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Users
{
    public class UserBorrowBookEvent : EventBase
    {
        public Guid UserAggregateRootId { get; set; }
        public Guid BookAggregateRootId { get; set; }
        public DateTime BorrowedDate { get; set; }
    }
}
