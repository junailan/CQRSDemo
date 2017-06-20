using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public abstract class EventBase : IEvent
    {

        public Guid AggregateRootId { get; set; }

        public string AggregateRootType { get; set; }

        public DateTime Timestamp { get; set; }

        public int Version { get; set; }

        public string EventType { get; set; }

        public string Data { get; set; }
    }
}
