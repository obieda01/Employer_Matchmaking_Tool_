using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using System.Configuration;
using Capstone.Web.Models.Data;

namespace Capstone.Web.Controllers
{
    public class StaffController : HomeController
    {
        private User loggedInUser = new User();

        public ActionResult StaffHome(string userName)
        {
            IUserDal idal = new UserSqlDal();
            loggedInUser = idal.GetUser(userName);
            return View();
        }

        // GET: Staff
        public ActionResult ViewEmployerSchedule()
        {
            //need to remove hard coding
            int matchmakingId = 1;

            CreateArrangementDropDownList();

            EmployerDAL edal = new EmployerDAL();

            List<Employer> employers = edal.GetAllEmployers(matchmakingId);

            List<SelectListItem> employerNames = new List<SelectListItem>();

            foreach (Employer e in employers)
            {
                employerNames.Add(new SelectListItem { Text = e.EmployerName, Value = e.EmployerId.ToString() });
            }

            ViewBag.EmployerNames = employerNames;

            InterviewDAL iDAL = new InterviewDAL();

            List<Interview> masterSchedule = iDAL.GetMasterSchedule(matchmakingId);

            return View(masterSchedule);

            //Note from KH: If we have to build two pages then - we will use this in the results view 
            //int employerId = 1;
            //InterviewDAL dal = new InterviewDAL();
            //List<Interview> employerSchedule = dal.GetEmployerSchedule(employerId);
            //return View(employerSchedule);

        }

        public ActionResult AssignRoom()
        {
            //remove hardcoding
            int matchmakingId = 1;

            CreateArrangementDropDownList();

            EmployerDAL eDAL = new EmployerDAL();
            List<EmployerTeam> employerList = eDAL.GetAllEmployersAndTeams(matchmakingId);
            
            return View(employerList);
        }

        public ActionResult UpdateRoom()
        {
            //remove hardcoding
            int matchmakingId = 1;

            EmployerDAL eDAL = new EmployerDAL();
            List<EmployerTeam> employerList = eDAL.GetAllEmployersAndTeams(matchmakingId);

            foreach (EmployerTeam e in employerList)
            {
                if (!String.IsNullOrEmpty(Request.Params[e.EmployerName + e.TeamId + "assignedRoom"]))
                {
                    e.AssignedRoom = Request.Params[e.EmployerName + e.TeamId + "assignedRoom"];
                }
                else
                {
                    e.AssignedRoom = "";
                }
            }

            bool isSuccessful = eDAL.UpdateAssignedRoom(employerList);

            if (isSuccessful)
            {
                ViewBag.Message = "The rooms were successfully updated.";
            }
            else
            {
                ViewBag.Message = "The room were not successfully updated. Please try again.";
            }

            return View("StaffHome");
        }

        public ActionResult ViewMasterSchedule()
        {
            //need to remove hardcoding
            int matchmakingId = 1;

            CreateArrangementDropDownList();

            InterviewDAL iDAL = new InterviewDAL();
            List<Interview> masterSchedule = iDAL.GetMasterSchedule(matchmakingId);

            return View(masterSchedule);
        }

        public ActionResult ViewAStudentsSchedule()
        {
            //need to remove hardcoding
            int matchmaking = 1;

            CreateArrangementDropDownList();

            StudentDAL sdal = new StudentDAL();

            List<Student> students = sdal.GetAllStudents();

            List<SelectListItem> studentNames = new List<SelectListItem>();

            foreach (Student s in students)
            {
                studentNames.Add(new SelectListItem { Text = s.StudentName, Value = s.StudentId.ToString() });
            }

            ViewBag.StudentNames = studentNames;

            InterviewDAL idal = new InterviewDAL();

            List<Interview> studentSchedules = idal.GetAllStudentsSchedules(matchmaking);

            return View(studentSchedules);
        }

        public ActionResult AddAStudentLogin()
        {
            CreateArrangementDropDownList();

            return View();
        }

        public ActionResult UpdateStudentLogin()
        {
            //need to add capability to email the student the username and password

            Student s = new Student();

            if ((!String.IsNullOrEmpty(Request.Params["studentName"])) && (!String.IsNullOrEmpty(Request.Params["userName"]))
                    && (!String.IsNullOrEmpty(Request.Params["languageId"])) && (!String.IsNullOrEmpty(Request.Params["matchmakingId"])))
            {
                s.StudentName = Request.Params["studentName"];
                s.UserName = Request.Params["userName"];
                s.LanguageId = int.Parse(Request.Params["languageId"]);
                s.MatchmakingId = int.Parse(Request.Params["matchmakingId"]);
            }

            StudentDAL sdal = new StudentDAL();

            bool isSuccessful = sdal.AddNewStudent(s);

            if (isSuccessful)
            {
                ViewBag.Message = "The student was successfully added.";
            }
            else
            {
                ViewBag.Message = "The student was not successfully added. Please try again.";  
            }

            // HERE Email Send

            return View("UpdateStatus");

        }

        public ActionResult AddEmployer()
        {
            return View();
        }

        public ActionResult UpdateEmployer()
        {
            Employer e = new Employer();

            if ((!String.IsNullOrEmpty(Request.Params["employerName"])) && (!String.IsNullOrEmpty(Request.Params["employerSummary"])))
            {

                e.EmployerName = Request.Params["employerName"];
                e.Summary = Request.Params["employerSummary"];
                e.NumberOfTeams = 1; //initially set this value to 1. Staff have the option to update it when they create an event
            }

            EmployerDAL edal = new EmployerDAL();

            bool isSuccessful = edal.AddNewEmployer(e);

            if (isSuccessful)
            {
                ViewBag.Message = "The employer was successfully added.";
            }
            else
            {
                ViewBag.Message = "The employer was not successfully added. Please try again.";
            }

            return View("StaffHome");
        }

        public ActionResult CreateANewArrangement()
        {
            MatchmakingArrangement arrangement = new MatchmakingArrangement();
            return View(arrangement);
        }

        public ActionResult UpdateArrangement()
        {

            MatchmakingArrangement arrangement = new MatchmakingArrangement();

            if ((!String.IsNullOrEmpty(Request.Params["season"])) && (!String.IsNullOrEmpty(Request.Params["location"]))
                    && (!String.IsNullOrEmpty(Request.Params["numberOfStudentChoices"])) && (!String.IsNullOrEmpty(Request.Params["cohortNumber"])))
            {
                arrangement.Location = Request.Params["location"];
                arrangement.Season = Request.Params["season"];
                arrangement.CohortNumber = int.Parse(Request.Params["cohortNumber"]);
                arrangement.NumberOfStudentChoices = int.Parse(Request.Params["numberOfStudentChoices"]);
            }

            EventDAL edal = new EventDAL();

            bool isSuccessful = edal.AddNewArrangement(arrangement);

            if (isSuccessful)
            {
                ViewBag.Message = "The arrangement was successfully added.";
            }
            else
            {
                ViewBag.Message = "The arrangement was not successfully added. Please try again.";
            }

            return View("StaffHome");

        }

        public void CreateArrangementDropDownList()
        {
            EventDAL edal = new EventDAL();

            List<MatchmakingArrangement> allArrangements = edal.GetAllArrangements();

            List<SelectListItem> arrangements = new List<SelectListItem>();

            foreach (MatchmakingArrangement a in allArrangements)
            {
                string displayText = a.Location + " Cohort " + a.CohortNumber + " " + a.Season;
                arrangements.Add(new SelectListItem { Text = displayText, Value = a.MatchmakingId.ToString() });
            }

            ViewBag.Arrangements = arrangements;
        }
    }
}