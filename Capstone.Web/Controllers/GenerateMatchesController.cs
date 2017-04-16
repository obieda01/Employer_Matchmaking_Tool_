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
        // GET: GenerateMatches
        public ActionResult MasterSchedule()
        {
            StudentChoiceDAL scDAL = new StudentChoiceDAL();
            InterviewDAL iDAL = new InterviewDAL();

            //need to remove hardcoding
            int matchmakingId = 1;

            for (int i = 1; i < 3; i++)
            {
                List<StudentChoice> StudentChoices = scDAL.GetEmployersByRank(i, matchmakingId);

                foreach (StudentChoice choice in StudentChoices)
                {
                    iDAL.GenerateMatchesByStudentRanking(choice);
                }
            }

            //randomly fill in the rest of the schedule
            iDAL.RandomlyGenerateRemainingSchedule(matchmakingId);

            List<Interview> masterSchedule = iDAL.GetMasterSchedule(matchmakingId);
            return View(masterSchedule);
        }
    }
}