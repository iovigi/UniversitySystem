namespace UniversitySystem.Web.Infrastructure.Extensions
{
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyModel;

    using Helpers;

    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register all types, which located in assemblies which contain assembliesContainName and implement interface TDependency
        /// </summary>
        /// <typeparam name="TDependency">Interface, which should be implemented by type</typeparam>
        /// <param name="services">Service collection</param>
        /// <param name="assembliesContainName">Assemblies name which should be contained</param>
        public static void RegisterAllDependenciesWhichImplement<TDependency>(this IServiceCollection services, string assembliesContainName)
        {
            var types = TypeHelpers.GetAllTypeForAllUsedAssemblyContainName(assembliesContainName)
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
