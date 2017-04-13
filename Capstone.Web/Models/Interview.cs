using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Interview
    {
        public string EmployerName { get; set; }
        public int EmployerId { get; set; }
        public int TeamId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string EventDate { get; set; }
        public string StartTime { get; set; }
    }
}