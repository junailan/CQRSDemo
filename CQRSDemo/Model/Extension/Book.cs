using Events.Books;
using Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Book : SourcedAggregateRoot
    {
        public static Book Create(string title, string author, string description, string isbn, int pages, int inventory)
        {
            Book book = new Book();
            book.RaiseEvent<BookCreateEvent>(new BookCreateEvent
            {
                AggregateRootId = Guid.NewGuid(),
                Title = title,
                Author = author,
                Description = description,
                ISBN = isbn,
                Pages = pages,
                Inventory = inventory
            });
            return book;
        }

        public void UpdateBook(string title, string author, string description,
            string isbn, int pages, int inventory)
        {
            this.RaiseEvent<BookUpdateEvent>(new BookUpdateEvent
            {
                AggregateRootId = this.AggregateRootId,
                Title = title,
                Author = author,
                Description = description,
                ISBN = isbn,
                Pages = pages,
                Inventory = inventory
            });
        }

        public void DeleteBook(Guid aggregateRootId)
        {
            this.RaiseEvent<BookDeleteEvent>(new BookDeleteEvent
            {
                AggregateRootId = aggregateRootId
            });
        }

        public void IssueStock(int numOfBooks)
        {
            if (Inventory < numOfBooks)
                throw new Exception(string.Format("库存不足. 当前库存: {0}.", Inventory));
            this.RaiseEvent<BookIssueStockEvent>(new BookIssueStockEvent
            {
                AggregateRootId = this.AggregateRootId,
                Quantity = numOfBooks
            });
        }

        public void ReceiveStock(int numOfBooks)
        {
            this.RaiseEvent<BookReceiveStockEvent>(new BookReceiveStockEvent
            {
                AggregateRootId = this.AggregateRootId,
                Quantity = numOfBooks,
            });
        }

        public void HandleEvent(BookCreateEvent e)
        {
            this.AggregateRootId = e.AggregateRootId;
            this.Title = e.Title;
            this.Author = e.Author;
            this.Description = e.Description;
            this.ISBN = e.ISBN;
            this.Pages = e.Pages;
            this.Inventory = e.Inventory;
        }

        public void HandleEvent(BookUpdateEvent e)
        {
            this.Title = e.Title;
            this.Author = e.Author;
            this.Description = e.Description;
            this.ISBN = e.ISBN;
            this.Pages = e.Pages;
            this.Inventory = e.Inventory;
        }

        public void HandleEvent(BookDeleteEvent e)
        {
            this.AggregateRootId = Guid.Empty;
            this.Title = null;
            this.Author = null;
            this.Description = null;
            this.ISBN = null;
            this.Pages = 0;
            this.Inventory = 0;
        }

        public void HandleEvent(BookIssueStockEvent e)
        {
            this.Inventory -= e.Quantity;
        }

        public void HandleEvent(BookReceiveStockEvent e)
        {
            this.Inventory += e.Quantity;
        }

        public override void DoBuildFromSnapshot(ISnapshot snapshot)
        {
            Book book = JsonConvert.DeserializeObject<Book>(snapshot.Data);
            this.AggregateRootId = book.AggregateRootId;
            this.Title = book.Title;
            this.Author = book.Author;
            this.Description = book.Description;
            this.Pages = book.Pages;
            this.Inventory = book.Inventory;
            this.ISBN = book.ISBN;
            this.Version = book.Version;
        }

        public override ISnapshot CreateSnapshot()
        {
            return new Snapshot
            {
                AggregateRootId = this.AggregateRootId,
                AggregateRootType = this.GetType().ToString(),
                Timestamp = DateTime.Now,
                Data = JsonConvert.SerializeObject(this)
            };
        }

    }
}
