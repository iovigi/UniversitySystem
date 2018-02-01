namespace UniversitySystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class StudentCourse
    {
        [Required]
        public string StudentId { get; set; }

        public Student Student { get; set; }

        [Required]
        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
