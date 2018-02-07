namespace UniversitySystem.Web.Infrastructure.Helpers
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyModel;

    public static class TypeHelpers
    {
        /// <summary>
        /// Get all types in assemblies related to project, which contain assembliesContainName
        /// </summary>
        /// <param name="assembliesContainName">assembliesContainName</param>
        /// <returns></returns>
        public static Type[] GetAllTypeForAllUsedAssemblyContainName(string assembliesContainName)
        {
            //INedelchev: This is only not buggy way so far, which I found to get all assemblies for solution.
            var dependencies = DependencyContext.Default.RuntimeLibraries;

            var types = dependencies
                .Where(d => d.Name.Contains(assembliesContainName))
                .Select(d => Assembly.Load(d.Name))
                .SelectMany(a => a.GetTypes());

            return types.ToArray();
        }
    }
}
