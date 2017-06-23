using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    public class ReturnBookCommand : CommandBase
    {
        public Guid UserAggregateRootId { get; set; }
        public Guid BookAggregateRootId { get; set; }
    }
}
