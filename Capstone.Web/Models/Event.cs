using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Event
    {
        public int MatchmakingId { get; set; }
        public string EventDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string LunchStart { get; set; }
        public string LunchEnd { get; set; }
        public string FirstBreakStart { get; set; }
        public string FirstBreakEnd { get; set; }
        public string SecondBreakStart { get; set; }
        public string SecondBreakEnd { get; set; }
        public int InterviewLength { get; set; }
        public int NumberOfInterviewSlots { get; set; }
        List <Student>AllStudents { get; set; }
        List <Student>ParticipatingStudents { get; set; }
        List<EmployerTeam>AllEmployers { get; set; }
        List<EmployerTeam>ParticipatingEmployers { get; set; }

    }
}