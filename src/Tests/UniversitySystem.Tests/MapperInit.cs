namespace UniversitySystem.Tests
{
    using AutoMapper;
    using UniversitySystem.Web.Infrastructure.Mapping;

    internal class MapperInit
    {
        private const string AssemblyUniversitySystem = "UniversitySystem";

        private static object syncRoot = new object();
        private static bool IsInit = false;

        /// <summary>
        /// Test is running in parallel, but map should be init once per domain. So this handle with race thread problem.
        /// </summary>
        public static void Init()
        {
            lock (syncRoot)
            {
                if (!IsInit)
                {
                    Mapper.Initialize(config => config.AddProfile(new AutoMapperProfile(AssemblyUniversitySystem)));
                    IsInit = true;
                }
            }
        }
    }
}
