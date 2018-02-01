namespace UniversitySystem.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using UniversitySystem.Data.Models;

    public class UniversitySystemDbContext : IdentityDbContext
    {
        public UniversitySystemDbContext(DbContextOptions options)
        : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<StudentCourse>()
                .HasKey(st => new { st.CourseId, st.StudentId });

            builder
                .Entity<StudentCourse>()
                .HasOne(studentCourse => studentCourse.Student)
                .WithMany(student => student.Courses)
                .HasForeignKey(studentCourse => studentCourse.StudentId);

            builder
                .Entity<StudentCourse>()
                .HasOne(studentCourse => studentCourse.Course)
                .WithMany(course => course.Students)
                .HasForeignKey(studentCourse => studentCourse.CourseId);
        }
    }
}
