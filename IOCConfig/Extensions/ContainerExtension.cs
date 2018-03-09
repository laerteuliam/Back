using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using SimpleInjector;
using System;

namespace IOCConfig.Extensions
{
    public static class ContainerExtension
    {
        public static void BatchRegister(this Container container, Assembly assembly, Func<Type, bool> @where, [Optional]Lifestyle lifestyle)
        {
            var registrations =
                from type in assembly.GetExportedTypes().Where(@where)
                where type.GetInterfaces().Any()
                where type.GetInterface(string.Format("I{0}", type.Name)) != null
                select new { Service = type.GetInterface(string.Format("I{0}", type.Name)), Implementation = type };


            if (lifestyle == null)
                foreach (var reg in registrations)
                    container.Register(reg.Service, reg.Implementation);
            else
                foreach (var reg in registrations)
                    container.Register(reg.Service, reg.Implementation, lifestyle);
        }

        public static void BatchRegister(this Container container, Assembly assembly, string nameSpace, [Optional]Lifestyle lifestyle)
        {
            BatchRegister(container, assembly, x => x.Namespace == nameSpace, lifestyle);
        }

        public static void BatchRegister<TService>(this Container container, [Optional]Lifestyle lifestyle)
            where TService : class
        {
            var type = typeof(TService);
            container.BatchRegister(type.Assembly, type.Namespace, lifestyle);
        }
    }
}
