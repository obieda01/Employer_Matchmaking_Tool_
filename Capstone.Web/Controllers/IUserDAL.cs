using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models.Data;
namespace Capstone.Web.DAL
{
    public interface IUserDal
    {
        User GetUser(string username, string password);
        User GetUser(string username);
        List<string> GetUsernames(string startsWith);

        bool CreateUser(User newUser);
        bool ChangePassword(string username, string newPassword);
    }
}