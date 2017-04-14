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
        public ActionResult StudentHome()
        {
            return View("StudentHome");
        }

        public ActionResult RankEmployers()
        {
            EmployerDAL edal = new EmployerDAL();
            List<Employer> results = new List<Employer>();
            results = edal.GetAllEmployers();
            return View(results);
        }
        public ActionResult ViewStudentSchedule()
        {
            //need to know how the student is getting transferred
            int studentId = 1;

            InterviewDAL dal = new InterviewDAL();

            List<Interview> studentSchedule = dal.GetStudentSchedule(studentId);

            return View(studentSchedule);
        }

    }
}