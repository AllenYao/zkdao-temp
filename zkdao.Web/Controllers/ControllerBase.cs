using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zkdao.Web.Extensions;
using zkdao.Web.UserServiceReference;

namespace zkdao.Web.Controllers {
    [HandleError]
    public class ControllerBase : Controller {
        protected Guid? CustomerID {
            get {
                //if (Session["CustomerID"] != null)
                //    return (Guid)Session["CustomerID"];
                //using (CustomerServiceClient client = new CustomerServiceClient())
                //{
                //    var customer = client.GetCustomerByUserName(User.Identity.Name);
                //    var customerID = new Guid(customer.ID);
                //    Session["CustomerID"] = customerID;
                //    return customerID;
                //}
                return new Guid();
            }
            set {
                Session["CustomerID"] = value;
            }
        }
    }
}