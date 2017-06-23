using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryService
{
    public class BookQueryService
    {
        public List<Book> GetBooks()
        {
            using (QueryDBEntities _dbContext = new QueryDBEntities())
            {
                return _dbContext.Book.OrderBy(t => t.Id).ToList();
            }
        }

        public Book GetBookById(Guid bookAggregateRootId)
        {
            using (QueryDBEntities _dbContext = new QueryDBEntities())
            {
                return _dbContext.Book.FirstOrDefault(t => t.AggregateRootId == bookAggregateRootId);
            }
        }
    }
}
