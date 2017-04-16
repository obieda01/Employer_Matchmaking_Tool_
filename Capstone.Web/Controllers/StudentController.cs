using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers.UsersProfiles
{
    public class StudentController : MatchMakingController
    {
        public StudentController(IUserDal userDal) :
            base(userDal)
        {
            //this.messageDal = messageDal;
        }


        // GET: Student
        public ActionResult StudentHome(string username)
        {
            ViewBag.userName = username;
            return View("StudentHome");
        }

        public ActionResult RankEmployers()
        {
            //need to update with the real matchmaking id
            int matchmakingId = 1;
            EmployerDAL edal = new EmployerDAL();

            List<Employer> employers = edal.GetAllEmployers();

            List<SelectListItem> employerNames = new List<SelectListItem>();

            foreach (Employer e in employers)
            {
                employerNames.Add(new SelectListItem { Text = e.EmployerName, Value = e.EmployerId.ToString() });
            }

            ViewBag.EmployerNames = employerNames;

            EventDAL eventDAL = new EventDAL();
            ViewBag.NumberOfStudentChoices = eventDAL.GetNumberOfStudentChoices(matchmakingId);
                 
            return View(employers);
        }

        public ActionResult ViewMySchedule()
        {
            //need to know how the student is getting transferred
            int studentId = 1;

            InterviewDAL dal = new InterviewDAL();

            List<Interview> studentSchedule = dal.GetStudentSchedule(studentId);

            return View(studentSchedule);
        }

        public ActionResult UpdateStudentChoices()
        {
            //need to update with the real student id and matchmaking id
            int studentId = 1;
            int matchmakingId = 1;

            StudentChoiceDAL scdal = new StudentChoiceDAL();
            EventDAL eventDAL = new EventDAL();
            int numberOfStudentChoices = eventDAL.GetNumberOfStudentChoices(matchmakingId);


            scdal.DeletePreviousChoices(studentId, matchmakingId);
            List<StudentChoice> studentChoices = new List<StudentChoice>();

            for (int i = 1; i<= numberOfStudentChoices; i++)
            {
                //StudentChoice s = new StudentChoice();
                //s.StudentId = studentId;
                //s.MatchmakingId = matchmakingId;
                //s.EmployerId = int.Parse(Request["Choice"+i]);
                //s.EmployerRank = i;
                //studentChoices.Add(s);
            }
          
            
            bool isSuccessful = scdal.UpdateStudentChoice(studentChoices);
            if (isSuccessful)
            {
                ViewBag.Message = "Your choices were submitted.";
            }
            else
            {
                ViewBag.Message = "Your choices were not submitted. Please try again.";
            }
            return View();
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

