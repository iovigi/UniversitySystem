namespace UniversitySystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [Range(1, 100)]
        public int Score { get; set; }

        public ICollection<StudentCourse> Students { get; set; }
    }
}
