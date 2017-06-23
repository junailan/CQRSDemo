using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Books
{
    public class DeleteBookCommand : CommandBase
    {
        public Guid BookAggregateRootId { get; set; }
    }
}
