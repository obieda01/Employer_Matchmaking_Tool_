using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class TimeSlot
    {
        public int MatchmakingId { get; set; }
        public int EventId { get; set; }
        public string EventDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int TimeSlotRank { get; set; }
    }
}