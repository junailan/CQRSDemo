using Commands;
using Commands.Books;
using Commands.Users;
using Framework;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Handler
{
    public class CommandHandler
    {
        private DomainRepository _domainRepository = new DomainRepository();
        protected DomainRepository DomainRepository
        {
            get
            {
                return _domainRepository;
            }
        }

        public void Handle(ICommand command)
        {
            dynamic d = this;
             d.HandleCommand(Converter.ChangeTo(command, command.GetType()));
        }

        public void HandleCommand(CreateUserCommand command)
        {
            User user = User.Create(command.UserName, command.Password, command.DisplayName, command.Email, command.ContactPhone, command.Address_Country, command.Address_State, command.Address_Street, command.Address_City, command.Address_Zip);
            DomainRepository.Save(user);
            DomainRepository.Commit();
        }

        public void HandleCommand(UpdateUserCommand command)
        {
            User user = DomainRepository.Get<User>(command.AggregateRootId);
            user.UpdateUser(command.DisplayName, command.Email, command.ContactPhone,
                command.Address_Country, command.Address_State, command.Address_Street,
                command.Address_City, command.Address_Zip);
            DomainRepository.Save(user);
            DomainRepository.Commit();
        }

        public void HandleCommand(CreateBookCommand command)
        {
            Book book = Book.Create(command.Title, command.Author, command.Description, command.ISBN, command.Pages, command.Inventory);
            DomainRepository.Save(book);
            DomainRepository.Commit();
        }

        public void HandleCommand(UpdateBookCommand command)
        {
            Book book = DomainRepository.Get<Book>(command.AggregateRootId);
            book.UpdateBook(command.Title, command.Author, command.Description, command.ISBN, command.Pages, command.Inventory);
            DomainRepository.Save(book);
            DomainRepository.Commit();
        }

        public void HandleCommand(DeleteBookCommand command)
        {
            Book book = DomainRepository.Get<Book>(command.AggregateRootId);
            book.DeleteBook(command.AggregateRootId);
            DomainRepository.Save(book);
            DomainRepository.Commit();

        }

        public void HandleCommand(BorrowBookCommand command)
        {
            User user = DomainRepository.Get<User>(command.UserAggregateRootId);
            Book book = DomainRepository.Get<Book>(command.BookAggregateRootId);
            user.BorrowBook(book);
            book.IssueStock(1);
            DomainRepository.Save(user);
            DomainRepository.Save(book);
            DomainRepository.Commit();
        }

        public void HandleCommand(ReturnBookCommand command)
        {
            User user = DomainRepository.Get<User>(command.UserAggregateRootId);
            Book book = DomainRepository.Get<Book>(command.BookAggregateRootId);
            user.ReturnBook(book);
            book.ReceiveStock(1);
            DomainRepository.Save(user);
            DomainRepository.Save(book);
            DomainRepository.Commit();
        }
    }
}
