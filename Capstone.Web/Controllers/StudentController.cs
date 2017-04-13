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
    }
}