using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public abstract class CommandBase : ICommand
    {
        public Guid AggregateRootId { get; set; }
    }
}
