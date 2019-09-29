using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;

namespace Nop.Plugin.Uga.Partners
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            //home page
            routes.MapRoute("Plugin.Uga.Partners.Index",
                 "partners/list/",
                 new { controller = "Partners", action = "Index" },
                 new[] { "Nop.Plugin.Uga.Partners.Controllers" }
            );


        }
        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}
