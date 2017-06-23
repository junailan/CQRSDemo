using Events.Books;
using Events.Users;
using Framework;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Handler
{
    public class EventHandler
    {
        public void Handle(object e)
        {
            dynamic d = this;
            Assembly assembly = Assembly.Load("Events");
            d.HandleEvent(Converter.ChangeTo(e, e.GetType()));
        }

        private void HandleEvent(UserCreateEvent domainEvent)
        {
            using (QueryDBEntities _dbContext = new QueryDBEntities())
            {
                User user = new User
                {
                    AggregateRootId = domainEvent.AggregateRootId,
                    UserName = domainEvent.UserName,
                    Password = domainEvent.Password,
                    DisplayName = domainEvent.DisplayName,
                    Email = domainEvent.Email,
                    ContactPhone = domainEvent.ContactPhone,
                    Address_Country = domainEvent.Address_Country,
                    Address_State = domainEvent.Address_State,
                    Address_Street = domainEvent.Address_Street,
                    Address_City = domainEvent.Address_City,
                    Address_Zip = domainEvent.Address_Zip
                };
                _dbContext.User.Add(user);
                _dbContext.SaveChanges();
            }
        }

        private void HandleEvent(UserUpdateEvent domainEvent)
        {
            using (QueryDBEntities _dbContext = new QueryDBEntities())
            {
                User user = _dbContext.User.FirstOrDefault(t => t.AggregateRootId == domainEvent.AggregateRootId);
                user.DisplayName = domainEvent.DisplayName;
                user.Email = domainEvent.Email;
                user.ContactPhone = domainEvent.ContactPhone;
                user.Address_Country = domainEvent.Address_Country;
                user.Address_State = domainEvent.Address_State;
                user.Address_Street = domainEvent.Address_Street;
                user.Address_City = domainEvent.Address_City;
                user.Address_Zip = domainEvent.Address_Zip;
                _dbContext.Entry(user).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
        }

        private void HandleEvent(UserBorrowBookEvent domainEvent)
        {
            using (QueryDBEntities _dbContext = new QueryDBEntities())
            {
                BorrowRecord record = new BorrowRecord
                {
                    UserAggregateRootId = domainEvent.UserAggregateRootId,
                    BookAggregateRootId = domainEvent.BookAggregateRootId,
                    BorrowedDate = domainEvent.BorrowedDate
                };
                _dbContext.BorrowRecord.Add(record);
                _dbContext.SaveChanges();
            }
        }

        private void HandleEvent(UserReturnBookEvent domainEvent)
        {
            using (QueryDBEntities _dbContext = new QueryDBEntities())
            {
                BorrowRecord record = _dbContext.BorrowRecord.FirstOrDefault(t => t.UserAggregateRootId == domainEvent.UserAggregateRootId && t.BookAggregateRootId == domainEvent.BookAggregateRootId && !t.Returned);
                record.Returned = true;
                record.ReturnedDate = domainEvent.ReturnedDate;
                _dbContext.Entry(record).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
        }

        private void HandleEvent(BookCreateEvent domainEvent)
        {
            using (QueryDBEntities _dbContext = new QueryDBEntities())
            {
                Book book = new Book
                {
                    AggregateRootId = domainEvent.AggregateRootId,
                    Title = domainEvent.Title,
                    Author = domainEvent.Author,
                    Description = domainEvent.Description,
                    ISBN = domainEvent.ISBN,
                    Pages = domainEvent.Pages,
                    Inventory = domainEvent.Inventory
                };
                _dbContext.Book.Add(book);
                _dbContext.SaveChanges();
            }
        }

        private void HandleEvent(BookUpdateEvent domainEvent)
        {
            using (QueryDBEntities _dbContext = new QueryDBEntities())
            {
                Book book = _dbContext.Book.FirstOrDefault(t => t.AggregateRootId == domainEvent.AggregateRootId);
                book.Title = domainEvent.Title;
                book.Author = domainEvent.Author;
                book.Description = domainEvent.Description;
                book.ISBN = domainEvent.ISBN;
                book.Pages = domainEvent.Pages;
                book.Inventory = domainEvent.Inventory;
                _dbContext.Entry(book).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
        }

        private void HandleEvent(BookDeleteEvent domainEvent)
        {
            using (QueryDBEntities _dbContext = new QueryDBEntities())
            {
                Book book = _dbContext.Book.FirstOrDefault(t => t.AggregateRootId == domainEvent.AggregateRootId);
                _dbContext.Entry(book).State = EntityState.Deleted;
                _dbContext.SaveChanges();
            }
        }

        private void HandleEvent(BookIssueStockEvent domainEvent)
        {
            using (QueryDBEntities _dbContext = new QueryDBEntities())
            {
                Book book = _dbContext.Book.FirstOrDefault(t => t.AggregateRootId == domainEvent.AggregateRootId);
                book.Inventory = book.Inventory - domainEvent.Quantity;
                _dbContext.Entry(book).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
        }

        private void HandleEvent(BookReceiveStockEvent domainEvent)
        {
            using (QueryDBEntities _dbContext = new QueryDBEntities())
            {
                Book book = _dbContext.Book.FirstOrDefault(t => t.AggregateRootId == domainEvent.AggregateRootId);
                book.Inventory = book.Inventory + domainEvent.Quantity;
                _dbContext.Entry(book).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
        }

    }
}
