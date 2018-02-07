namespace UniversitySystem.Web.Models.Course
{
    using System.Collections.Generic;

    using AutoMapper;

    using Business.Services.Models.Courses;
    using Common.Mapping;

    public class CourseListsByStudentViewModel : IMapFrom<CourseListsByStudentServiceModel>, IHaveCustomMapping
    {
        public string StudentId { get; set; }

        public List<CourseViewModel> RegisteredCourses { get; set; }

        public List<CourseViewModel> NotRegisteredCourses { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<CourseListsByStudentServiceModel, CourseListsByStudentViewModel>()
                 .ForMember(d => d.RegisteredCourses, opt => opt.MapFrom(s => s.RegisteredCourses))
                 .ForMember(d => d.NotRegisteredCourses, opt => opt.MapFrom(s => s.NotRegisteredCourses)); ;
        }
    }
}
