using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public interface IEvent
    {
        Guid AggregateRootId { get; set; }

        string AggregateRootType { get; set; }

        DateTime Timestamp { get; set; }

        int Version { get; set; }

        string EventType { get; set; }

        string Data { get; set; }
    }
}
