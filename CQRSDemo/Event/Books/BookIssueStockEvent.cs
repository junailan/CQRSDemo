using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Books
{
    public class BookIssueStockEvent : EventBase
    {
        public int Quantity { get; set; }
    }
}
