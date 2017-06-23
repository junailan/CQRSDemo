using Events.Users;
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
        public static User Create(string userName, string password, string displayName, string email, string contactPhone, string address_Country, string address_State, string address_Street, string address_City, string address_Zip)
        {
            User user = new User();
            user.RaiseEvent<UserCreateEvent>(new UserCreateEvent()
            {
                AggregateRootId = Guid.NewGuid(),
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
                AggregateRootId = this.AggregateRootId,
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
            if (this._bookIds.Contains(book.AggregateRootId))
                throw new Exception("你已经借过这本书了.");
            this.RaiseEvent<UserBorrowBookEvent>(new UserBorrowBookEvent
            {
                AggregateRootId = this.AggregateRootId,
                UserAggregateRootId = this.AggregateRootId,
                BookAggregateRootId = book.AggregateRootId,
                BorrowedDate = DateTime.Now
            });
        }

        public void ReturnBook(Book book)
        {
            if (!this._bookIds.Contains(book.AggregateRootId))
                throw new Exception("你没有借过这本书或已经归还.");
            this.RaiseEvent<UserReturnBookEvent>(new UserReturnBookEvent
            {
                AggregateRootId = this.AggregateRootId,
                UserAggregateRootId = this.AggregateRootId,
                BookAggregateRootId = book.AggregateRootId,
                ReturnedDate = DateTime.Now
            });
        }

        private List<Guid> _bookIds = new List<Guid>();

        public List<Guid> BookIds
        {
            get
            {
                return _bookIds;
            }
            set
            {
                _bookIds = value;
            }
        }

        public void HandleEvent(UserCreateEvent e)
        {
            this.AggregateRootId = e.AggregateRootId;
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

        public void HandleEvent(UserBorrowBookEvent e)
        {
            this._bookIds.Add(e.BookAggregateRootId);
        }

        public void HandleEvent(UserReturnBookEvent e)
        {
            this._bookIds.Remove(e.BookAggregateRootId);
        }

        public void HandleEvent(UserUpdateEvent e)
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
            this.AggregateRootId = user.AggregateRootId;
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
            this.Version = user.Version;
            this.BookIds = user.BookIds;
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
