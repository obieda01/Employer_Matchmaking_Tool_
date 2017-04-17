using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Capstone.Web.Tests.Controllers
{
    [TestClass]
    public class AboutControllerTests
    {
        [TestMethod()]
        public void AboutController_AboutAction_ReturnAboutView()
        {
            AboutController controller = new AboutController();

            ViewResult result = controller.About() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("About", result.ViewName);
        }
    }
}
