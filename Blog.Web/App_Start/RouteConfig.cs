using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
      name: "Tim kiem",
      url: "tim-kiem.html",
      defaults: new { controller = "Post", action = "Search", id = UrlParameter.Optional },
      namespaces: new string[] { "Blog.Web.Controllers" }
  );

            routes.MapRoute(
        name: "Tin Game",
        url: "tin-game.html",
        defaults: new { controller = "Post", action = "TinGame", id = UrlParameter.Optional },
        namespaces: new string[] { "Blog.Web.Controllers" }
    );

            routes.MapRoute(
      name: "Tin ESports",
      url: "tin-esports.html",
      defaults: new { controller = "Post", action = "TinESports", id = UrlParameter.Optional },
      namespaces: new string[] { "Blog.Web.Controllers" }
  );
            routes.MapRoute(
    name: "Cam nang",
    url: "cam-nang.html",
    defaults: new { controller = "Post", action = "Camnang", id = UrlParameter.Optional },
    namespaces: new string[] { "Blog.Web.Controllers" }
);
            routes.MapRoute(
   name: "Cong dong",
   url: "cong-dong.html",
   defaults: new { controller = "Post", action = "Congdong", id = UrlParameter.Optional },
   namespaces: new string[] { "Blog.Web.Controllers" }
);

            routes.MapRoute(
 name: "Video",
 url: "video.html",
 defaults: new { controller = "Post", action = "video", id = UrlParameter.Optional },
 namespaces: new string[] { "Blog.Web.Controllers" }
);

            routes.MapRoute(
name: "Hinh anh",
url: "hinh-anh.html",
defaults: new { controller = "Post", action = "Hinhanh", id = UrlParameter.Optional },
namespaces: new string[] { "Blog.Web.Controllers" }
);

            routes.MapRoute(
  name: "ListByTag",
  url: "tag/{tagId}.html",
  defaults: new { controller = "Post", action = "ListByTag", tagId = UrlParameter.Optional },
  namespaces: new string[] { "Blog.Web.Controllers" }
);


            routes.MapRoute(
  name: "Category",
  url: "{alias}.pc-{id}.html",
  defaults: new { controller = "Post", action = "Category", alias = UrlParameter.Optional },
  namespaces: new string[] { "Blog.Web.Controllers" }
);

            routes.MapRoute(
 name: "Detail",
 url: "{alias}.p-{productId}.html",
 defaults: new { controller = "Post", action = "Detail", productId = UrlParameter.Optional },
 namespaces: new string[] { "Blog.Web.Controllers" }
);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Blog.Web.Controllers" }
            );
        }
    }
}