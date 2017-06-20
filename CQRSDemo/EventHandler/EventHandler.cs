using Event.Books;
using Event.Users;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event
{
    public class EventHandler
    {


        public void Handle(UserCreateEvent domainEvent)
        {
            using (QueryDBEntities _dbContext = new QueryDBEntities())
            {
                
            }
        }

        public void Handle(UserUpdateEvent domainEvent)
        {

        }

        public void Handle(UserBorrowBookEvent domainEvent)
        {

        }

        public void Handle(UserReturnBookEvent domainEvent)
        {

        }

        public void Handle(BookCreateEvent domainEvent)
        {

        }

        public void Handle(BookUpdateEvent domainEvent)
        {

        }

        public void Handle(BookDeleteEvent domainEvent)
        {

        }

        public void Handle(BookIssueStockEvent domainEvent)
        {

        }

        public void Handle(BookReceiveStockEvent domainEvent)
        {

        }

    }
}
