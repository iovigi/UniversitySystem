namespace UniversitySystem.Web.Models.Course
{
    using System.ComponentModel.DataAnnotations;

    using Business.Services.Models.Courses;
    using Common;
    using Common.Mapping;

    public class CourseViewModel : IMapFrom<CourseServiceModel>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(GlobalCourseConstants.MinNameLength)]
        public string Name { get; set; }

        [Required]
        [Range(GlobalCourseConstants.MinCoursePoint, GlobalCourseConstants.MaxCoursePoint)]
        public int Score { get; set; }
    }
}
