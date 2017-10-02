using Autofac;
using Autofac.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Uga.Partners.Controllers;
using Nop.Plugin.Uga.Partners.Data;
using Nop.Plugin.Uga.Partners.Domain;
using Nop.Plugin.Uga.Partners.Services;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Uga.Partners.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string CONTEXT_NAME = "nop_object_context_uga_partners";

        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<PartnerService>().As<IPartnerService>().InstancePerLifetimeScope();

            this.RegisterPluginDataContext<ObjectContext>(builder, CONTEXT_NAME);

            builder.RegisterType<EfRepository<PartnersRecord>>()
                .As<IRepository<PartnersRecord>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_uga_partners"))
                .InstancePerLifetimeScope();

            builder.RegisterType<PartnersController>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
        }



        public int Order
        {
            get { return 2; }
        }
    }
}
