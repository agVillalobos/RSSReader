using System;
namespace RSSFeed.Models
{
    public class User
    {
        public User(
            string userName,
            string password,
            string email,
            string surname,
            string lastname,
            string address,
            string phone)
        {
            UserName = userName;
            Password = password;
            Email = email;
            Surname = surname;
            LastName = lastname;
            Address = address;
            Phone = phone;
        }

        public Guid Id { get => Guid.NewGuid(); }
        public string UserName { get; }
        public string Password { get; }
        public string Email { get; }
        public string Surname { get; }
        public string LastName { get; }
        public string Address { get; }
        public string Phone { get; }
    }
}
