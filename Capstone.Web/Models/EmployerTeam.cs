using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class EmployerTeam
    {
        public int EmployerId { get; set; }
        public int TeamId { get; set; }
        public string EventDate { get; set; }
        public string Language { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string AssignedRoom { get; set; }
        public string EmployerName { get; set; }
        public List<Interview> EmployerTeamSchedule { get; set; }
    }
}