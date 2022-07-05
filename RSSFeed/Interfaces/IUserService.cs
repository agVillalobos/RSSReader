using System;
using System.Threading.Tasks;
using RSSFeed.Models;

namespace RSSFeed.Interfaces
{
    public interface IUserService
    {
        Task<bool> SignIn(string userName, string password);
        Task<bool> SignUp(User user);
    }
}
