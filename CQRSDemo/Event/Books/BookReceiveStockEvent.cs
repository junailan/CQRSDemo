using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Books
{
    public class BookReceiveStockEvent : EventBase
    {
        public int Quantity { get; set; }
    }
}
