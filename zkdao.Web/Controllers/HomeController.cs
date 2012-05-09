using System.Web.Mvc;
using zkdao.Web.Extensions;

namespace zkdao.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

    }
}
