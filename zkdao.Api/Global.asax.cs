﻿using System;
using System.Web;

namespace zkdao.Api {

    public class Global : HttpApplication {

        private void Application_Start(object sender, EventArgs e) {
            RegisterRoutes();
        }

        private void RegisterRoutes() {
            // Edit the base address of Service1 by replacing the "Service1" string below
            //RouteTable.Routes.Add(new ServiceRoute("Service1", new WebServiceHostFactory(), typeof(Service1)));
        }
    }
}