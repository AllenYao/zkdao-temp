using System.Web.Mvc;
using zkdao.Web.Extensions;
using zkdao.Web.ProductServiceReference;

namespace zkdao.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            using (ProductServiceClient productServiceClient = new ProductServiceClient())
            {
                var products = productServiceClient.GetLaptops();
                return View(products);
            }
        }

        public ActionResult About()
        {
            return View();
        }

    }
}
