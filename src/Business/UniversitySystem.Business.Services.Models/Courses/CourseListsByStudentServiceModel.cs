namespace UniversitySystem.Business.Services.Models.Courses
{
    using System.Collections.Generic;
    using System.Linq;

    public class CourseListsByStudentServiceModel
    {
        public CourseListsByStudentServiceModel()
        {
            this.RegisteredCourses = new List<CourseServiceModel>();
            this.NotRegisteredCourses = new List<CourseServiceModel>();
        }

        public CourseListsByStudentServiceModel(string studentId)
            :this()
        {
            this.StudentId = studentId;
        }

        public string StudentId { get; set; }

        public List<CourseServiceModel> RegisteredCourses { get; set; }

        public List<CourseServiceModel> NotRegisteredCourses { get; set; }
    }
}
