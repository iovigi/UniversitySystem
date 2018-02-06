namespace UniversitySystem.Business.Services.Models.Students
{
    using AutoMapper;

    using Common.Mapping;
    using Data.Models;

    public class StudentServiceModel : IMapFrom<Student>, IHaveCustomMapping
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<Student, StudentServiceModel>()
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.UserName));
        }
    }
}
