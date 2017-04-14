using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class StudentChoice
    {
        public int StudentId { get; set; }
        public int EmployerId { get; set; }
        public int EmployerRank { get; set; }
        public string EventDate { get; set; }
    }
}