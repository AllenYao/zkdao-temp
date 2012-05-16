using System.Web.Mvc;
using System.Web.Routing;
using zic_dotnet;

namespace zkdao.Web {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode,
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication {

        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            UrlBuild.JsHead = "/Scripts/";
            UrlBuild.JsHeadDeploy = "/Scripts/__build/";
            UrlBuild.CssHead = "/Content/";
            UrlBuild.CssHeadDeploy = "/Content/__build/";
            UrlBuild.ImgHead = "/Content/images/";
            UrlBuild.ImgHeadDeploy = "/Content/images/";
            UrlBuild.ControlDeploy = UrlBuild.eControlDeploy.NoDeploy;
        }
    }
}