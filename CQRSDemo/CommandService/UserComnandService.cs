using Commands;
using Commands.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandService
{
    public class UserComnandService : CommandService
    {
        public void RegisterUser(string userName, string password, string displayName, string email, string contactPhone, string address_Country, string address_State, string address_Street, string address_City, string address_Zip)
        {
            CreateUserCommand command = new CreateUserCommand
            {
                UserName = userName,
                Password = password,
                DisplayName = displayName,
                Email = email,
                ContactPhone = contactPhone,
                Address_Country = address_Country,
                Address_State = address_State,
                Address_Street = address_Street,
                Address_City = address_City,
                Address_Zip = address_Zip,
            };
            CommitCommand(command);
        }

        public void UpdateUser(Guid aggregateRootId, string displayName, string email, string contactPhone, string address_Country, string address_State, string address_City, string address_Street, string address_Zip)
        {
            UpdateUserCommand command = new UpdateUserCommand
            {
                AggregateRootId = aggregateRootId,
                DisplayName = displayName,
                Email = email,
                ContactPhone = contactPhone,
                Address_Country = address_Country,
                Address_State = address_State,
                Address_City = address_City,
                Address_Street = address_Street,
                Address_Zip = address_Zip
            };
            CommitCommand(command);
        }


        public void BorrowBookToUser(Guid userAggregateRootId, Guid bookAggregateRootId)
        {
            BorrowBookCommand command = new BorrowBookCommand
            {
                UserAggregateRootId = userAggregateRootId,
                BookAggregateRootId = bookAggregateRootId
            };
            CommitCommand(command);
        }

        public void ReturnBookFromUser(Guid userAggregateRootId, Guid bookAggregateRootId)
        {
            ReturnBookCommand command = new ReturnBookCommand
            {
                UserAggregateRootId = userAggregateRootId,
                BookAggregateRootId = bookAggregateRootId
            };
            CommitCommand(command);
        }
    }
}
