using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;
using System.Globalization;
using System.Configuration;

namespace Capstone.Web.DAL
{
    
        public class CreateEventDAL
        {
            private string connectionString = ConfigurationManager.ConnectionStrings["FinalCapstoneDatabase"].ConnectionString;

        }
    
}