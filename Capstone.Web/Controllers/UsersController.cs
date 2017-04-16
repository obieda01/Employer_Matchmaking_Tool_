using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models.Users;
using Capstone.Web.DAL;
using Capstone.Web.Crypto;

namespace Capstone.Web.Controllers
{

    public class UsersController : MatchMakingController
    {

        private readonly IUserDal userDal;

        public UsersController(IUserDal userDal)
              : base(userDal)
        {
            this.userDal = userDal;
        }

        // GET: Login
        [HttpGet]
        [Route("login")]
        public ActionResult Login()
        {
            if (base.IsAuthenticated)
            {

                return RedirectToAction("AdminHome", "Admin", new { username = base.CurrentUser });
            }

            var model = new LoginViewModel();
            return View("Login", model);
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = userDal.GetUser(model.Username);


                if (user != null)
                {
                    HashProvider hashProvider = new HashProvider();
                    bool doesPasswordMatch = hashProvider.VerifyPasswordMatch(user.Password, model.Password, "123456789abcdefg"); //user.Salt)

                    if (!doesPasswordMatch)
                    {
                        ModelState.AddModelError("invalid-login", "The username or password combination is not valid");
                        return View("Login", model);
                    }
                }
                else
                {
                    ModelState.AddModelError("invalid-login", "The username or password combination is not valid");
                    return View("Login", model);
                }

                // Happy Path
                base.LogUserIn(user.Username);


                //If they are supposed to be redirected then redirect them else send them to the dashboard
                var queryString = this.Request.UrlReferrer.Query;
                var urlParams = HttpUtility.ParseQueryString(queryString);
                if (urlParams["landingPage"] != null && Url.IsLocalUrl(urlParams["landingPage"]))
                {
                    // then redirect them
                    return new RedirectResult(urlParams["landingPage"]);
                }
                else if (urlParams["landingPage"] != null && !Url.IsLocalUrl(urlParams["landingPage"]))
                {
                    return RedirectToAction("LeavingSite", "Users", new { destinationUrl = urlParams["landingPage"] });
                }
                else
                {
                   // return RedirectToAction("StaffHome", "Staff", new { username = base.CurrentUser });        //HERE


                    if (user.User_Role == "admin"|| (user.User_Role == "staff"))
                    {
                        return RedirectToAction("StaffHome", "Staff", new {username = base.CurrentUser});
                    }
                    else if(user.User_Role=="student")
                    {
                        return RedirectToAction("StudentHome", "Student", new { username = base.CurrentUser });
                    }
                    else
                    {
                        return View("Login", model);

                    }

                }

            }
            else
            {
                return View("Login", model);
            }
        }


        public ActionResult ChangePassword(string username)
        {
            var model = new ChangePasswordViewModel();
            return View("ChangePassword", model);
        }

        [HttpGet]
        [Route("logout")]
        public ActionResult Logout()
        {
            base.LogUserOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult LeavingSite()
        {
            return View("LeavingSite");
        }

    }
}