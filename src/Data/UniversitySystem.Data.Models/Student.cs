namespace UniversitySystem.Data.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class Student : IdentityUser
    {
        public Student()
        {
            this.Courses = new HashSet<StudentCourse>();
        }

        public ICollection<StudentCourse> Courses { get; set; }
    }
}
