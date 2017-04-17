﻿using System;
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
        public Student loggedInStudent = new Student() {
            StudentId=1,
            MatchmakingId=1
        };

        public StudentController(IUserDal userDal) :
            base(userDal)
        {
           
        }

        // GET: Student
        public ActionResult StudentHome(string userName)
        {
            //StudentDAL sdal = new StudentDAL();
            //loggedInStudent = sdal.GetStudent(userName);
            ViewBag.userName = userName;
            return View("StudentHome",loggedInStudent);
        }

        public ActionResult RankEmployers()
        {
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

        public ActionResult ViewMySchedule()
        {
            InterviewDAL dal = new InterviewDAL();

            List<Interview> studentSchedule = dal.GetStudentSchedule(loggedInStudent.StudentId, loggedInStudent.MatchmakingId);

            return View(studentSchedule);
        }

        public ActionResult UpdateStudentChoices()
        {
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
            if (isSuccessful)
            {
                ViewBag.Message = "Your choices were submitted.";
            }
            else
            {
                ViewBag.Message = "Your choices were not submitted. Please try again.";
            }
            return View("StudentHome");
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

