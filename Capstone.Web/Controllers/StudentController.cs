using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using Capstone.Web.Models.Data;

namespace Capstone.Web.Controllers.UsersProfiles
{
    public class StudentController : MatchMakingController
    {
        public StudentController(IUserDal userDal) :
            base(userDal)
        {
           
        }

        public int x = 0;
        // GET: Student
        public ActionResult StudentHome(string userName)
        {
            Student loggedInStudent = GetLoggedInStudent(userName);
            Session["UserName"] = userName;
            return View("StudentHome", loggedInStudent);
        }

        public ActionResult RankEmployers(string userName)
        {
            Student loggedInStudent = GetLoggedInStudent(userName);

            EmployerDAL edal = new EmployerDAL();

            List<Employer> employers = edal.GetAllEmployers(loggedInStudent.MatchmakingId);

            List<SelectListItem> employerNames = new List<SelectListItem>();

            foreach (Employer e in employers)
            {
                employerNames.Add(new SelectListItem { Text = e.EmployerName, Value = e.EmployerId.ToString() });
            }

            ViewBag.EmployerNames = employerNames;

            EventDAL eventDAL = new EventDAL();

            ViewBag.NumberOfStudentChoices = eventDAL.GetNumberOfStudentChoices(loggedInStudent.MatchmakingId);
                 
            return View(employers);
        }

        public ActionResult ViewMySchedule(string userName)
        {
            InterviewDAL idal = new InterviewDAL();

            Student loggedInStudent = GetLoggedInStudent(userName);

            List<Interview> studentSchedule = idal.GetStudentSchedule(loggedInStudent.StudentId, loggedInStudent.MatchmakingId);

            return View(studentSchedule);
        }

        public ActionResult UpdateStudentChoices(string userName)
        {
            System.Collections.Specialized.NameValueCollection parameters = Url.RequestContext.HttpContext.Request.Params;

            string userName2 = Request.Params["userName"];

            Student loggedInStudent = GetLoggedInStudent(userName);

            StudentChoiceDAL scdal = new StudentChoiceDAL();

            EventDAL eventDAL = new EventDAL();

            int numberOfStudentChoices = eventDAL.GetNumberOfStudentChoices(loggedInStudent.MatchmakingId);
            
            scdal.DeletePreviousChoices(loggedInStudent.StudentId, loggedInStudent.MatchmakingId);

            List<StudentChoice> studentChoices = new List<StudentChoice>();

            for (int i = 1; i<= numberOfStudentChoices; i++)
            {
                StudentChoice s = new StudentChoice();
                s.StudentId = loggedInStudent.StudentId;
                s.MatchmakingId = loggedInStudent.StudentId;
                s.EmployerId = int.Parse(Request["Choice" + i]);
                s.EmployerRank = i;
                studentChoices.Add(s);
            }
          
            bool isSuccessful = scdal.UpdateStudentChoice(studentChoices);

            ViewBag.Message = (isSuccessful) ? "Your choices were successfully added." : "Your choices were was not successfully added. Please try again.";

            return View("StudentHome", loggedInStudent);
        }

        private Student GetLoggedInStudent(string userName)
        {
            ViewBag.userName = userName;

            StudentDAL sdal = new StudentDAL();

            return (sdal.GetStudent(userName));
        }

        [ChildActionOnly]
        public ActionResult GetAuthenticatedUser()
        {
            //User model = null;

            //if (IsAuthenticated)
            //{
            //    model = userDal.GetUser(CurrentUser);
            //}

            return PartialView("ViewMySchedule");
        }

    }
}

