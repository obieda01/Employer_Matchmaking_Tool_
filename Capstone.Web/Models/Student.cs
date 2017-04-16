using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Capstone.Web.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Language { get; set; }
        public int LanguageId { get; set; }
        public string UserName { get; set; }
        public List<Interview> StudentSchedule { get; set; }
        public List<StudentChoice> EmployerRankings { get; set; }

    }

}