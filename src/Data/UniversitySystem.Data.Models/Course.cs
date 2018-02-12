namespace UniversitySystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class Course
    {
        public Course()
        {
            this.Students = new HashSet<StudentCourse>();
        }

        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(GlobalCourseConstants.MinNameLength)]
        public string Name { get; set; }

        [Required]
        [Range(GlobalCourseConstants.MinCoursePoint, GlobalCourseConstants.MaxCoursePoint)]
        public int Score { get; set; }

        public ICollection<StudentCourse> Students { get; set; }
    }
}
