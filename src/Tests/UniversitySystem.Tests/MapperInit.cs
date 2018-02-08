namespace UniversitySystem.Tests
{
    using AutoMapper;
    using Web.Infrastructure.Mapping;

    public class MapperInit
    {
        private const string AssemblyUniversitySystem = "UniversitySystem";

        private static object syncRoot = new object();

        private static bool IsInit = false;

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
