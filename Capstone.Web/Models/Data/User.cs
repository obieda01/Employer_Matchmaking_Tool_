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
        public int ProfileId { get; set; }
        public string SendEmail()
        {
            return "";
        }
    }
}