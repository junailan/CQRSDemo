using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public interface ISnapshot
    {
        Guid AggregateRootId { get; set; }

        string AggregateRootType { get; set; }

        int Version { get; set; }

        string Data { get; set; }

        DateTime Timestamp { get; set; }
    }
}
