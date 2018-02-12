namespace UniversitySystem.Web.Models.Course
{
    using Business.Services.Models.Courses;
    using Common.Mapping;

    public class RegisterToCourseViewModel : IMapFrom<RegisterToCourseServiceModel>
    {
        public bool IsSuccessfull { get; set; }
        public bool CanRegisterMore { get; set; }
    }
}
