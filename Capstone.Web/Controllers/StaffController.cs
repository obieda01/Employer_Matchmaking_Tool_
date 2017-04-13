﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using System.Configuration;

namespace Capstone.Web.Controllers
{
    public class StaffController : HomeController
    {

        // GET: Staff
        public ActionResult ViewEmployerSchedule()
        {
            //need to pull the employer id from the previous page
            int employerId = 1;

            InterviewDAL dal = new InterviewDAL();

            List<Interview> employerSchedule = dal.GetEmployerSchedule(employerId);

            return View(employerSchedule);

        }

        public ActionResult ViewMasterSchedule()
        {

            StudentChoiceDAL scDAL = new StudentChoiceDAL();
            InterviewDAL iDAL = new InterviewDAL();

            List<Interview> masterSchedule = new List<Interview>();

            for (int i = 1; i < 3; i++)
            {
                List<StudentChoice> StudentChoices = scDAL.GetEmployersByRank(i);

                foreach (StudentChoice choice in StudentChoices)
                {
                    iDAL.UpdateEmployerScheduleWithStudent(choice);
                }

                //randomly fill in the rest of the schedule
                iDAL.RandomlyFillInRemainingEmployerSchedules();

                masterSchedule = iDAL.GetMasterSchedule();
            }

            return View(masterSchedule);
        }

        public ActionResult CreateEvent()
        {
            return null;
        }
    }
}