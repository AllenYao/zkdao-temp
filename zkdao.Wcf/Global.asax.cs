using System;
using System.Data.Entity;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using log4net;
using zic_dotnet;
using zkdao.Application;
using zkdao.Repositories.EF;

namespace zkdao.Wcf {

    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {
            BaseApplication.Initialize();
            Database.SetInitializer<EFDbContext>(new EFDbContextInitializer());
            ZicLog4Net.Instance.Config(new string[] { "Domain", "AppService", "InfrasEmail", "InfrasEF", "InfrasWCF", "InfrasREST" }, new ZicGmailConfig());
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