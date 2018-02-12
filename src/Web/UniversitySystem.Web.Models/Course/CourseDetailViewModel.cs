namespace UniversitySystem.Web.Models.Course
{
    using Business.Services.Models.Courses;
    using Common.Mapping;

    public class CourseDetailViewModel : IMapFrom<CourseServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Score { get; set; }

        public int CountOfStudent { get; set; }

        public bool CanBeModified
        {
            get
            {
                return this.CountOfStudent == 0;
            }
        }
    }
}
