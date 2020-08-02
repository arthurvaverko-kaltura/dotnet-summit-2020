using KLogMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace ApiApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static readonly KLogger _Logger = new KLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString());

        protected void Application_Start()
        {
            KLogger.Configure("log4net.config");

            _Logger.Info("Starting application");
            GlobalConfiguration.Configure(WebApiConfig.Register);
            _Logger.Info("Registered routes");

            ControllerBuilder.Current.DefaultNamespaces.Add("ApiControllers");
        }
    }
}
