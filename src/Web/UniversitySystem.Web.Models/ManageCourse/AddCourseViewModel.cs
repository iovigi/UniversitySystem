namespace UniversitySystem.Web.Models.ManageCourse
{
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class AddCourseViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(GlobalCourseConstants.MinNameLength)]
        public string Name { get; set; }

        [Required]
        [Range(GlobalCourseConstants.MinCoursePoint, GlobalCourseConstants.MaxCoursePoint)]
        public int Score { get; set; }

        public bool IsSucessfull { get; set; }
    }
}
