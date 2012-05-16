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
                if (Session["UserID"] != null)
                    return (Guid)Session["UserID"];
                using (UserServiceClient client = new UserServiceClient()) {
                    var user = client.UserGetByKey(User.Identity.Name);
                    var customerID = new Guid(user.ID);
                    Session["CustomerID"] = customerID;
                    return customerID;
                }
            }
            set {
                Session["UserID"] = value;
            }
        }
    }
}