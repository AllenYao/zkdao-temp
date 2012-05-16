using System.Web.Mvc;

namespace zkdao.Web.Controllers {

    public class HomeController : ControllerBase {

        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            return View();
        }
    }
}