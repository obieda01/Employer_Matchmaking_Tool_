using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models.Data;

namespace Capstone.Web.Controllers
{
    public class AdminController : MatchMakingController
    {

        public AdminController( IUserDal userDal) :
            base(userDal)
        {
            //this.messageDal = messageDal;
        }

        // GET: Admin
        public ActionResult AdminHome()
        {
            return View("AdminHome");
        }

        public ActionResult SendEmailHome()
        {
            User s=new User();
            string xx= s.SendEmail();
            return View("AdminHome");
        }

        // GET: Admin
        public ActionResult AddStaff()
        {
            return View("AddStaff");
        }

    }
}
