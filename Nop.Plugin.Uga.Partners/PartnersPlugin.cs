using System.Web.Routing;
using Nop.Core.Plugins;
using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Plugin.Uga.Partners.Data;
using Nop.Services.Security;

namespace Nop.Plugin.Uga.Partners
{
    public class PartnersPlugin : BasePlugin, IMiscPlugin
    {

        private ObjectContext _context;
        private readonly IPermissionService _permissionService;

        public PartnersPlugin(ObjectContext context, IPermissionService permissionService)
        {
            this._permissionService = permissionService;
            _context = context;
        }



        #region Methods

        /// <summary>
        /// Gets a route for provider configuration
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
			controllerName = "Partners";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Uga.Partners.Controllers" }, { "area", null } };
        }

        public override void Install()
        {
            _context.Install();
            

            this.AddOrUpdatePluginLocaleResource("Uga.Partners.Search", "Search partner with Postal Code or City", "en-US");
            this.AddOrUpdatePluginLocaleResource("Uga.Partners.Search", "Αναζητήστε συνεργάτη με πόλη ή ΤΚ", "el-GR");
            this.AddOrUpdatePluginLocaleResource("Uga.Partners.Search", "Search partner with Postal Code or City", "ru-RU");
            this.AddOrUpdatePluginLocaleResource("Uga.Partners.Search", "Search partner with Postal Code or City", "it-IT");
            this.AddOrUpdatePluginLocaleResource("Uga.Partners.Search", "Search partner with Postal Code or City", "es-ES");
            this.AddOrUpdatePluginLocaleResource("Uga.Partners.Search", "Search partner with Postal Code or City", "fr-FR");

            base.Install();
        }

        public override void Uninstall()
        {
            _context.Uninstall();
            //uninstall permissions
            

            //locales
            this.DeletePluginLocaleResource("Plugin.Uga.AjaxFilters.Filters");
            this.DeletePluginLocaleResource("Plugins.Uga.WebServices.Description1");
            this.DeletePluginLocaleResource("Plugins.Uga.WebServices.Description2");
            this.DeletePluginLocaleResource("Plugins.Uga.WebServices.Description3");
            this.DeletePluginLocaleResource("Plugins.Uga.WebServices.Description4");


            base.Uninstall();
        }
        #endregion
    }
}
