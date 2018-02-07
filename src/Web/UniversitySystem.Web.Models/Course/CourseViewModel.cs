namespace UniversitySystem.Web.Models.Course
{
    using System.ComponentModel.DataAnnotations;

    using Business.Services.Models.Courses;
    using Common.Mapping;

    public class CourseViewModel : IMapFrom<CourseServiceModel>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [Range(1, 100)]
        public int Score { get; set; }
    }
}
