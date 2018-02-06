namespace UniversitySystem.Business.Services.Models.Courses
{
    using AutoMapper;

    using Common.Mapping;
    using Data.Models;

    public class CourseServiceModel : IMapFrom<Course>, IHaveCustomMapping
    {
        public CourseServiceModel()
        {
        }

        public CourseServiceModel(int id, string name, int score, int countOfStudent)
        {
            this.Id = id;
            this.Name = name;
            this.Score = score;
            this.CountOfStudent = countOfStudent;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int CountOfStudent { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<Course, CourseServiceModel>()
                .ForMember(d => d.CountOfStudent, opt => opt.MapFrom(s => s.Students.Count));
        }
    }
}
