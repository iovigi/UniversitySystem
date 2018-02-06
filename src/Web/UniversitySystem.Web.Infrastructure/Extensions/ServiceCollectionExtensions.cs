namespace UniversitySystem.Web.Infrastructure.Extensions
{
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyModel;

    public static class ServiceCollectionExtensions
    {
        public static void RegisterAllDependenciesWhichImplement<TDependency>(this IServiceCollection services, string assembliesContainName)
        {
            //INedelchev: This is only not buggy way so far, which I found to get all assemblies for solution.
            var dependencies = DependencyContext.Default.RuntimeLibraries;

            var types = dependencies
                .Where(d => d.Name.Contains(assembliesContainName))
                .Select(d => Assembly.Load(d.Name))
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && t.GetInterfaces().Contains(typeof(TDependency)));

            foreach (var type in types)
            {
                foreach (var serviceType in type.GetInterfaces().Where(x => x != typeof(TDependency)))
                {
                    services.AddTransient(serviceType, type);
                }
            }
        }
    }
}
