using Pony.Client.Service;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace Pony.Client
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IGameService, GameService>();
            container.RegisterType<IRobotEngine, RobotEngine>();
            container.RegisterType<IPathService, PathService>();
            container.RegisterType<IRestfulClient, RestfulClient>(new InjectionFactory(c => new RestfulClient("PonyEndPoint")));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}