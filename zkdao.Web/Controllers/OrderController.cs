using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zkdao.Web.Extensions;
using zkdao.Web.OrderServiceReference;

namespace zkdao.Web.Controllers
{
    public class OrderController : ControllerBase
    {
        [Authorize]
        public ActionResult Confirm(string orderID)
        {
            Guid orderGUID = new Guid(orderID);
            using (OrderServiceClient orderServiceClient = new OrderServiceClient())
            {
                orderServiceClient.Confirm(orderGUID);
            }
            return Json(null);
        }

        [Authorize]
        public ActionResult Details(string orderID)
        {
            Guid orderGUID = new Guid(orderID);
            using (OrderServiceClient orderServiceClient = new OrderServiceClient())
            {
                SalesOrderDataObject salesOrderDataObject = orderServiceClient.GetSalesOrder(orderGUID);
                return View(salesOrderDataObject);
            }
        }
    }
}
