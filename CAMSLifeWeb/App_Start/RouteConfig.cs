using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CaliphWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
       //     routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
       //     routes.MapRoute(
       //    name: "PersitencyCalculator",
       //    url: "{controller}/{action}/{agentid}",
       //    defaults: new { controller = "Persistency", action = "Calculator", agentid = UrlParameter.Optional }
       //);
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Payment",
               url: "{controller}/{action}/{id}/{paymentChannel}",
               defaults: new { controller = "Payment", action = "Result", paymentChannel = UrlParameter.Optional }
           );

            routes.MapRoute(
            name: "MapaCalculator",
            url: "{controller}/{action}/{type}/{id}",
            defaults: new { controller = "Agent", action = "MapaCalculator", id = UrlParameter.Optional }
        );

            routes.MapRoute(
             name: "StripePaymentResult",
             url: "{controller}/{action}/{id}/{paymentChannel}",
             defaults: new { controller = "Payment", action = "StripePaymentResult", paymentChannel = UrlParameter.Optional }
         );
        }
    }
}
