using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Biz4CMS
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

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );



            routes.MapRoute(
              name: "Article",
              url: "a/{pageURL}",
              defaults: new { controller = "Article", action = "Details", pageURL = UrlParameter.Optional,id= UrlParameter.Optional },
              namespaces: new String[] { "Biz4CMS.Controllers" }
           );

            routes.MapRoute(
               name: "ArticleCategory",
                url: "c/{pageURL}",
                defaults: new { controller = "Article", action = "Index", pageURL = UrlParameter.Optional },
                namespaces: new String[] { "Biz4CMS.Controllers" }
            );

            routes.MapRoute(
              name: "ProductCategory",
               url: "s/{pageURL}",
               defaults: new { controller = "Product", action = "Index", pageURL = UrlParameter.Optional },
               namespaces: new String[] { "Biz4CMS.Controllers" }
           );

            routes.MapRoute(
              name: "product",
              url: "p/{pageURL}",
              defaults: new { controller = "Product", action = "Details", pageURL = UrlParameter.Optional },
              namespaces: new String[] { "Biz4CMS.Controllers" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new String[] { "Biz4CMS.Controllers" }

            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}