using Commands.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandService
{
    public class BookCommandService : CommandService
    {
        public void AddBook(string title, string author, string description, string isbn, int pages, int inventory)
        {
            CreateBookCommand command = new CreateBookCommand
            {
                Title = title,
                Author = author,
                Description = description,
                ISBN = isbn,
                Pages = pages,
                Inventory = inventory
            };
            CommitCommand(command);
        }

        public void UpdateBook(Guid aggregateRootId, string title, string author, string description,
            string isbn, int pages, int inventory)
        {
            UpdateBookCommand command = new UpdateBookCommand
            {
                AggregateRootId = aggregateRootId,
                Title = title,
                Author = author,
                Description = description,
                ISBN = isbn,
                Pages = pages,
                Inventory = inventory
            };
            CommitCommand(command);
        }

        public void DeleteBook(Guid bookAggregateRootId)
        {
            DeleteBookCommand command = new DeleteBookCommand
            {
                BookAggregateRootId = bookAggregateRootId
            };
            CommitCommand(command);
        }

    }
}
