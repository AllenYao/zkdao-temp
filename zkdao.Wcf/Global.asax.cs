using System;
using log4net;
using zkdao.Application;

namespace zkdao.Wcf {

    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {
            BaseApplication.Initialize();
            log4net.Config.XmlConfigurator.Configure();
            Log = log4net.LogManager.GetLogger("ZKdao.Wcf");
        }

        protected void Session_Start(object sender, EventArgs e) {
        }

        protected void Application_BeginRequest(object sender, EventArgs e) {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) {
        }

        protected void Application_Error(object sender, EventArgs e) {
        }

        protected void Session_End(object sender, EventArgs e) {
        }

        protected void Application_End(object sender, EventArgs e) {
        }

        public static ILog Log { get; set; }
    }
}