using System;
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
            EmployerDAL edal = new EmployerDAL();
            List<Employer> employers = new List<Employer>();
            employers = edal.GetAllEmployers();

            List<SelectListItem> employerNames = new List<SelectListItem>();
            foreach (Employer e in employers)
            {
                employerNames.Add(new SelectListItem { Text = e.EmployerName, Value = e.EmployerId.ToString() });
            }

            ViewBag.EmployerNames = employerNames;


            InterviewDAL iDAL = new InterviewDAL();
            List<Interview> masterSchedule = iDAL.GetMasterSchedule();
            return View(masterSchedule);

            //Note from KH: If we have to build two pages then - we will use this in the results view 
            //int employerId = 1;
            //InterviewDAL dal = new InterviewDAL();
            //List<Interview> employerSchedule = dal.GetEmployerSchedule(employerId);
            //return View(employerSchedule);

        }

        public ActionResult ViewMasterSchedule()
        {
            InterviewDAL iDAL = new InterviewDAL();
            List<Interview> masterSchedule = iDAL.GetMasterSchedule();
            return View(masterSchedule);
        }

        public ActionResult ViewAStudentsSchedule()
        {
            //need to know how the student is getting transferred
            int studentId = 1;

            InterviewDAL dal = new InterviewDAL();

            List<Interview> studentSchedule = dal.GetStudentSchedule(studentId);

            return View(studentSchedule);
        }


        public ActionResult CreateAStudentLogin()
        {

            return View();
        }

        public ActionResult CreateEvent()
        {
            return null;
        }
    }
}