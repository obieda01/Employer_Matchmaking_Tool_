using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class MatchmakingArrangement
    {
        public int MatchmakingId { get; set; }
        public string Location { get; set; }
        public string Season { get; set; }
        public int CohortNumber { get; set; }
        public int NumberOfStudentChoices { get; set; }

    }
}