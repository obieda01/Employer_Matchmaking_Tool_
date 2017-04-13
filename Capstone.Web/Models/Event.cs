using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Event
    {
        public DateTime EventDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime LunchStart { get; set; }
        public DateTime LunchEnd { get; set; }
        public DateTime FirstBreakStart { get; set; }
        public DateTime FirstBreakEnd { get; set; }
        public DateTime SecondBreakStart { get; set; }
        public DateTime SecondBreakEnd { get; set; }
        public int InterviewLenght { get; set; }
        public int AmountOfInterviews { get; set; }

    }
}