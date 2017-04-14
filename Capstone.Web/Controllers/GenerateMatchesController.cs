using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class GenerateMatchesController : Controller
    {
        public string connectionstring = ConfigurationManager.ConnectionStrings["FinalCapstoneDatabase"].ConnectionString;
        // GET: GenerateMatches
        public ActionResult Index()
        {
            //StudentChoiceDAL dal = new StudentChoiceDAL(connectionstring);
            //List<StudentChoice> StudentChoices = dal.GetEmployersByRank(1);

            InterviewDAL dal = new InterviewDAL();
            List<Interview> masterSchedule = dal.GetMasterSchedule();

            return View(masterSchedule);
        }
    }
}