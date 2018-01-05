using Autofac;
using Autofac.Integration.Mvc;
using JqTest.DAL;
using JqTest.Services;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JqTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<JqContext>().AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<PersonService>()
              .AsSelf()
              .InstancePerLifetimeScope();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
