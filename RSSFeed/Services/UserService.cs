using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSSFeed.Interfaces;
using RSSFeed.Models;

namespace RSSFeed.Services
{
    /// <summary>
    /// Fake user service.
    /// </summary>
    public class UserService: IUserService
    {
        private IList<User> users;

        public UserService()
        {
            var defaultUser = new User("Test", "1234", "test@gmail.com", "Test", "Garcia", "test", "+1 444 4444444");
            users = new List<User>() { defaultUser };
        }

        public Task<bool> SignIn(string userName, string password)
        {
            var firstUser = users.FirstOrDefault(currentUser => currentUser.UserName == userName && currentUser.Password == password);

            return Task.FromResult(firstUser != null);
        }

        public Task<bool> SignUp(User user)
        {
            users.Add(user);

            return Task.FromResult(true);
        }
    }
}
