using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Web.Mvc;
using zkdao.Web;
using zkdao.Web.Controllers;
using zkdao.Web.UserServiceReference;
using zic_dotnet;
using Moq;

namespace zkdao.Test {

    //http://blog.zhaojie.me/2009/04/cannot-have-too-many-helper-methods.html
    [TestClass]
    public class AccountTest {

        [TestMethod]
        public void LogOnTest() {
            //UserIdentity identity = new UserIdentity();

            Mock<HomeController> mockController = new Mock<HomeController>() { CallBase = true };
            //mockController.Setup(c => c.Identity).Returns(identity);

            var result = mockController.Object.Index().Is<ViewResult>().IsView(null, null);
            //var model = result.ViewData.Model.Is<IndexModel>();

            //Assert.AreEqual(identity, model.Identity);
            //Assert.AreEqual("Welcome to ASP.NET MVC!", model.Message);
        }
    }
}