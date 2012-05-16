using System.Web.Mvc;

namespace zkdao.Api.Web.Controllers {

    public class HomeController : Controller {

        public ActionResult Index() {
            ViewBag.Message = "欢迎使用 ASP.NET MVC!";

            return View();
        }

        public ActionResult About() {
            return View();
        }
    }
}