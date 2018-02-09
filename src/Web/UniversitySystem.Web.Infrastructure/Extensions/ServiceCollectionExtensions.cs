namespace UniversitySystem.Web.Infrastructure.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;

    using Helpers;

    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register all types, which located in assemblies which contain assembliesContainName and implement interface TDependency
        /// </summary>
        /// <typeparam name="TDependency">Interface, which should be implemented by type</typeparam>
        /// <param name="services">Service collection</param>
        /// <param name="assembliesContainName">Assemblies name which should be contained</param>
        public static void RegisterAllNonGenericDependenciesWhichImplement<TDependency>(this IServiceCollection services, string assembliesContainName)
        {
            var types = TypeHelpers.GetAllTypeForAllUsedAssemblyContainName(assembliesContainName)
                .Where(t => t.IsClass && t.GetInterfaces().Contains(typeof(TDependency)));

            foreach (var type in types.Where(t => !t.IsGenericType))
            {
                foreach (var serviceType in type.GetInterfaces().Where(x => x.GetInterfaces().Any(t => t == typeof(TDependency))))
                {
                    services.AddTransient(serviceType, type);
                }
            }
        }

        /// <summary>
        /// Register all generic dependencies. Workaround for .NET core issue https://github.com/aspnet/Home/issues/2341
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="genericTypes">Dictionary -> (serviceType -> implementedType) -> All type, which can be generic</param>
        public static void RegisterGenericDependencies(this IServiceCollection services, IDictionary<KeyValuePair<Type, Type>, Type[]> genericTypes)
        {
            if (genericTypes == null)
            {
                return;
            }

            foreach (var types in genericTypes)
            {
                var serviceType = types.Key.Key;
                var implementedType = types.Key.Value;
                var possibleGenericTypes = types.Value;

                if (possibleGenericTypes == null)
                {
                    continue;
                }

                foreach (var possibleGenericType in possibleGenericTypes)
                {
                    services.AddTransient(serviceType.MakeGenericType(possibleGenericType), implementedType.MakeGenericType(possibleGenericType));
                }
            }
        }
    }
}
