using FSL.CyclomaticComplexity.Models.Novo.Services;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FSL.CyclomaticComplexity
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RegisterDependencyResolver();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void RegisterDependencyResolver()
        {
            var container = new Container();
            container.Register<IFactory, Factory>(Lifestyle.Singleton);
            container.Register<IStatusNegociacaoFactory, StatusNegociacaoFactory>(Lifestyle.Singleton);
            container.Register<ITipoDataFactory, TipoDataFactory>(Lifestyle.Singleton);
            container.Register<IHelperService, HelperService>(Lifestyle.Singleton);
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
