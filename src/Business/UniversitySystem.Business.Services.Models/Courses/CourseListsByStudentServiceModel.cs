namespace UniversitySystem.Business.Services.Models.Courses
{
    using System.Linq;

    public class CourseListsByStudentServiceModel
    {
        public string StudentId { get; set; }

        public IQueryable<CourseServiceModel> RegisteredCourses { get; set; }

        public IQueryable<CourseServiceModel> NotRegisteredCourses { get; set; }
    }
}
