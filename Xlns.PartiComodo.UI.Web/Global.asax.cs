using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Xlns.PartiComodo.UI.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Homepage", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        //protected void Session_Start()
        //{
        //    Session.setItemsPerPage(ConfigurationManager.Configurator.Istance.itemsPerPage);
        //}

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ConfigurationManager.Configurator.configFileName = AppDomain.CurrentDomain.BaseDirectory + @"Config\BusBook.config";

            log4net.Config.XmlConfigurator.Configure();
        }
    }
}