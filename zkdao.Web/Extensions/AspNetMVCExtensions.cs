using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace zkdao.Web.Extensions {
    public static class AspNetMVCExtensions {
        public static string SalesOrderStatusText(this HtmlHelper htmlHelper, int? orderStatus) {
            if (orderStatus == null)
                return MvcHtmlString.Empty.ToHtmlString();

            switch (orderStatus) {
                case 0:
                    return "Created";
                case 1:
                    return "Paid";
                case 2:
                    return "Picked";
                case 3:
                    return "Dispatched";
                case 4:
                    return "Delivered";
                default:
                    return "UNKNOWN";
            }
        }
    }
}
