using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.Data
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string User_Role { get; set; }
        public int UserId { get; set; }
        public string StaffName { get; set; }
        public string SendEmail()
        {
            return "";
        }
        
    }
}