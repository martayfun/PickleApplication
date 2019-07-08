using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PickleApplication.Web
{
    [RoutePrefix("Category")]
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               "Icerik",
               "icerik/{id}",
               new { controller = "Content", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               "Urun Listesi",
               "urun-listesi/{id}",
               new { controller = "Product", action = "ProductList", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              "Urun Detayi",
              "urun-detayi/{id}",
              new { controller = "Product", action = "ProductDetail", id = UrlParameter.Optional }
            );

            routes.MapRoute(
             "Sepetim",
             "sepetim/{id}",
             new { controller = "Shopping", action = "ShoppingCard", id = UrlParameter.Optional }
            );

            routes.MapRoute(
             "Siparis Detayi",
             "siparis-detayı/{id}",
             new { controller = "Shopping", action = "ShoppingDetail", id = UrlParameter.Optional }
            );

            routes.MapRoute(
             "Siparis Sonucu",
             "siparis-sonucu/{id}",
             new { controller = "Shopping", action = "ShoppingResult", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
