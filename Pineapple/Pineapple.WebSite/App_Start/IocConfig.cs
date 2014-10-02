using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pineapple.Core;
using Pineapple.Service;
using Microsoft.Practices.Unity;

namespace Pineapple.WebSite.App_Start
{
    public class IocConfig
    {
        public static void RegisterTypes()
        {
            ApplicationInitialService.RegisterContainer();

            Container.UnityContainer.RegisterType<IControllerActivator, ControllerActivator>()
                .RegisterType<IDependencyResolver, UnityDependencyResolver>();

            DependencyResolver.SetResolver(new UnityDependencyResolver());
        }
    }

    public class UnityDependencyResolver : IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            return Container.CanResolve(serviceType) ? Container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.CanResolve(serviceType) ? Container.ResolveAll(serviceType) : new object[0];
        }
    }

    public class ControllerActivator : IControllerActivator
    {
        public IController Create(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return DependencyResolver.Current.GetService(controllerType) as IController;
        }
    }
}