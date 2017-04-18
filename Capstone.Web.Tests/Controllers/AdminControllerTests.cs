using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models.Data;
namespace Capstone.Web.Tests.Controllers
{
    [TestClass]
    public class AdminControllerTests
    {
        private readonly IUserDal userDal;

        [TestMethod()]
        public void AdminController_AdminHomeAction_ReturnAdminHomeView()
        {
            AdminController controller = new AdminController(userDal);

            ViewResult result = controller.AdminHome() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("AdminHome", result.ViewName);
        }

        [TestMethod()]
        public void AdminController_SendEmailHomeAction_ReturnAdminHomeView()
        {
            AdminController controller = new AdminController(userDal);

            ViewResult result = controller.SendEmailHome() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("AdminHome", result.ViewName);
        }

        [TestMethod()]
        public void AdminController_SendEmailHomeActionvg_ReturnAdminHomeView()
        {
            //AdminController controller = new AdminController(userDal);

            //ViewResult result = controller.SendEmailHome() as ViewResult;

            //Assert.IsNotNull(result);
            //Assert.AreEqual("AdminHome", result.ViewName);
        }
    }
}
