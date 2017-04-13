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
        public DateTime EventDate { get; set; }
        public string Language { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string AssignedRoom { get; set; }
        public List<Interview> EmployerTeamSchedule { get; set; }
    }
}