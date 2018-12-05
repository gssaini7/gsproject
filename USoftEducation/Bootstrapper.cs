using System.Web.Http;
using Microsoft.Practices.Unity;
using System.Web.Mvc;
using R.Resolver;
using R.BAL;
using System;

namespace R.Web
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            //System.Web.Mvc.DependencyResolver.SetResolver(new Unity.Mvc3.UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            ComponentLoader.LoadContainer(container, ".\\bin", "R.BAL.dll");
        }
    }
}