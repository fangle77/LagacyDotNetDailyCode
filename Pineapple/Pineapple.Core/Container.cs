using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Pineapple.Core
{
    public class Container
    {
        private static readonly UnityContainer unityContainer = new UnityContainer();
        private static readonly string DomainDirectory = System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');

        public static UnityContainer UnityContainer { get { return unityContainer; } }

        public static void ResisterAssemblyType(params string[] assemblyNames)
        {
            if (assemblyNames == null) return;

            foreach (var assemblyName in assemblyNames)
            {
                string assemblyFile = string.Format("{0}\\{1}.dll", DomainDirectory, assemblyName);
                Assembly assembly = Assembly.LoadFrom(assemblyFile);
                foreach (var type in assembly.GetTypes())
                {
                    if (type.Namespace == null || !type.Namespace.StartsWith(assemblyName, StringComparison.OrdinalIgnoreCase)) continue;
                    if (type.IsAbstract) continue;
                    if (type.IsInterface) continue;
                    if (type.ContainsGenericParameters) continue;
                    if (!type.IsClass) continue;

                    var instance = PolicyInjection.Create(type, type);// Activator.CreateInstance(type);

                    unityContainer.RegisterInstance(type, instance, new ContainerControlledLifetimeManager());
                    unityContainer.BuildUp(type, instance);
                }
            }
        }

        public static T Resolve<T>()
        {
            return unityContainer.Resolve<T>();
        }
    }
}
