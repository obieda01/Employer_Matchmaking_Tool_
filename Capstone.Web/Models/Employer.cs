using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Employer
    {
        public int EmployerId { get; set; }
        public string EmployerName { get; set; }
        public int NumberOfTeams { get; set; }
        public string Summary{ get; set; }
    }
}