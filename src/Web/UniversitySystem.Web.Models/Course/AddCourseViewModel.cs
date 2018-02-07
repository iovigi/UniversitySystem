namespace UniversitySystem.Web.Models.Course
{
    using System.ComponentModel.DataAnnotations;

    public class AddCourseViewModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [Range(1, 100)]
        public int Score { get; set; }
    }
}
