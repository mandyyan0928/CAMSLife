using CaliphWeb.Core.Helper;
using CaliphWeb.Helper;
using CaliphWeb.Helper.ALCData;
using CaliphWeb.Services;
using CaliphWeb.Services.Helper;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace CaliphWeb
{
    public static class UnityConfig
    {
        [assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            container.RegisterType<IRestHelper, HttpClientHelper>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IMasterDataService, MasterDataService>();
            container.RegisterType<ICaliphAPIHelper, CaliphAPIHelper>();
            container.RegisterType<IALCApiHelper, SLMAPIHelper>();
            container.RegisterType<IALCDataGetter, SunlifeDataGetter>();
        }
    }
}