namespace UniversitySystem.Web.Infrastructure.Mapping
{
    using System;
    using System.Linq;

    using AutoMapper;

    using Common.Mapping;
    using Infrastructure.Helpers;

    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Automapper profile wich register all types, which implement IMapFrom in assemblies related to project, which contain assembliesContainName
        /// </summary>
        /// <param name="assembliesContainName">assembliesContainName</param>
        public AutoMapperProfile(string assembliesContainName)
        {
            var allTypes = TypeHelpers.GetAllTypeForAllUsedAssemblyContainName(assembliesContainName);

            allTypes
                .Where(t => t.IsClass && !t.IsAbstract && t
                    .GetInterfaces()
                    .Where(i => i.IsGenericType)
                    .Select(i => i.GetGenericTypeDefinition())
                    .Contains(typeof(IMapFrom<>)))
                .Select(t => new
                {
                    Destination = t,
                    Source = t
                        .GetInterfaces()
                        .Where(i => i.IsGenericType)
                        .Select(i => new
                        {
                            Definition = i.GetGenericTypeDefinition(),
                            Arguments = i.GetGenericArguments()
                        })
                        .Where(i => i.Definition == typeof(IMapFrom<>))
                        .SelectMany(i => i.Arguments)
                        .First(),
                })
                .ToList()
                .ForEach(mapping => this.CreateMap(mapping.Source, mapping.Destination));

            allTypes
                .Where(t => t.IsClass
                    && !t.IsAbstract
                    && typeof(IHaveCustomMapping).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<IHaveCustomMapping>()
                .ToList()
                .ForEach(mapping => mapping.ConfigureMapping(this));
        }
    }
}
