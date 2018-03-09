using CommonServiceLocator;
using EntityFrameworkConfig;
using IOCConfig.Adapters;
using IOCConfig.Extensions;
using ProjetoContext.DomainModels;
using SharedKernel.Helpers.Repository;
using SimpleInjector;
using System.Data.Entity;
using System.Reflection;

namespace IOCConfig
{
    public static class Initialize
    {
        public static void Bootstrap(this Container container, Lifestyle lifestyle)
        {
            container.Register<IRepository, Repository>(lifestyle);
            container.Register<DbContext, AppDbContext>(lifestyle);

            var assemblies = new[]
            {
                typeof(Usuario).Assembly,
                typeof(Produto).Assembly
            };

            for (int i = 0; i < assemblies.Length; i++)
                RegisterComumByAssemblies(container, assemblies[i], lifestyle);

            ServiceLocator.SetLocatorProvider(() => new SimpleInjectorServiceLocatorAdapter(container));
        }

        private static void RegisterComumByAssemblies(Container container, Assembly assembly, Lifestyle lifestyle)
        {
            container.BatchRegister(assembly, x => x.Name.Contains("Repository"), lifestyle);
            container.BatchRegister(assembly, x => x.Name.Contains("Service"), lifestyle);
        }
    }
}
