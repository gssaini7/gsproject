using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using System.Web.Http;
using Microsoft.Practices.Unity;
using System.Web.Mvc;
using R.Resolver;

using System;

namespace R.Web
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

      return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
      var container = new UnityContainer();

      // register all your components with the container here
      // it is NOT necessary to register your controllers

      // e.g. container.RegisterType<ITestService, TestService>();    
      RegisterTypes(container);

      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
            ComponentLoader.LoadContainer(container, ".\\bin", "R.BAL.dll");
            container.RegisterType<USoftEducation.Controllers.AccountController>(new InjectionConstructor());
            //container.RegisterType<USoftEducation.Controllers.AdminController>(new InjectionConstructor());
        }
  }
}