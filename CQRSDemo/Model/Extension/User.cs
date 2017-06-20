using Event.Users;
using Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class User : SourcedAggregateRoot
    {
        public User() : base() { }
        public User(Guid id) : base(id) { }

        public static User Create(string userName, string password, string displayName, string email, string contactPhone, string address_Country, string address_State, string address_Street, string address_City, string address_Zip)
        {
            User user = new User();
            user.RaiseEvent<UserCreateEvent>(new UserCreateEvent()
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
                Address_Zip = address_Zip
            });
            return user;
        }

        public void UpdateUser(string displayName, string email, string contactPhone,
            string country, string state, string street, string city, string zip)
        {
            this.RaiseEvent<UserUpdateEvent>(new UserUpdateEvent
            {
                DisplayName = displayName,
                Email = email,
                ContactPhone = contactPhone,
                Address_Country = country,
                Address_State = state,
                Address_Street = street,
                Address_City = city,
                Address_Zip = zip
            });
        }

        public void BorrowBook(Book book)
        {
            if (this.bookIds.Contains(book.AggregateRootId))
                throw new Exception("You've already borrowed this book.");
            this.RaiseEvent<UserBorrowBookEvent>(new UserBorrowBookEvent
            {
                UserAggregateRootId = this.AggregateRootId,
                BookAggregateRootId = book.AggregateRootId,
                BorrowedDate = DateTime.Now
            });
        }

        public void ReturnBook(Book book)
        {
            if (!this.bookIds.Contains(book.AggregateRootId))
                throw new Exception("I have not borrowed this book yet.");
            this.RaiseEvent<UserReturnBookEvent>(new UserReturnBookEvent
            {
                UserAggregateRootId = this.AggregateRootId,
                BookAggregateRootId = book.AggregateRootId,
                ReturnedDate = DateTime.Now
            });
        }

        private List<Guid> bookIds = new List<Guid>();

        private void HandleEvent(UserCreateEvent e)
        {
            this.UserName = e.UserName;
            this.Password = e.Password;
            this.DisplayName = e.DisplayName;
            this.Email = e.Email;
            this.ContactPhone = e.ContactPhone;
            this.Address_Country = e.Address_Country;
            this.Address_State = e.Address_State;
            this.Address_City = e.Address_City;
            this.Address_Street = e.Address_Street;
            this.Address_Zip = e.Address_Zip;
        }

        private void HandleEvent(UserBorrowBookEvent e)
        {
            this.bookIds.Add(e.BookAggregateRootId);
        }

        private void HandleEvent(UserReturnBookEvent e)
        {
            this.bookIds.Remove(e.BookAggregateRootId);
        }

        private void HandleEvent(UserUpdateEvent e)
        {
            this.DisplayName = e.DisplayName;
            this.Email = e.Email;
            this.ContactPhone = e.ContactPhone;
            this.Address_Country = e.Address_Country;
            this.Address_City = e.Address_City;
            this.Address_State = e.Address_State;
            this.Address_Street = e.Address_Street;
            this.Address_Zip = e.Address_Zip;
        }

        public override void DoBuildFromSnapshot(ISnapshot snapshot)
        {
            User user = JsonConvert.DeserializeObject<User>(snapshot.Data);
            this.UserName = user.UserName;
            this.Password = user.Password;
            this.Email = user.Email;
            this.DisplayName = user.DisplayName;
            this.ContactPhone = user.ContactPhone;
            this.Address_Country = user.Address_Country;
            this.Address_City = user.Address_City;
            this.Address_State = user.Address_State;
            this.Address_Street = user.Address_Street;
            this.Address_Zip = user.Address_Zip;
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
