namespace UniversitySystem.Web.Models.Course
{
    using Business.Services.Models.Courses;
    using Common.Mapping;

    public class UnRegisterToCourseViewModel : IMapFrom<UnRegisterToCourseServiceModelcs>
    {
        public bool IsSuccessfull { get; set; }
        public bool CanRegisterMore { get; set; }
    }
}
