using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zkdao.Web.Extensions;
using zkdao.Web.ProductServiceReference;
using zkdao.Web.OrderServiceReference;

namespace zkdao.Web.Controllers
{
    public class ProductController : ControllerBase
    {
        //
        // GET: /Product/

        public ActionResult Details(Guid ID)
        {
            using (ProductServiceClient productServiceClient = new ProductServiceClient())
            {
                var product = productServiceClient.GetLaptop(ID);
                return View(product);
            }
            
        }

        [Authorize]
        public ActionResult AddToCart(Guid productID)
        {
            using (OrderServiceClient orderServiceClient = new OrderServiceClient())
            {
                orderServiceClient.AddProductToCart(CustomerID.Value, productID);
            }
            return RedirectToAction("ShoppingCart", "Account");
        }
    }
}
