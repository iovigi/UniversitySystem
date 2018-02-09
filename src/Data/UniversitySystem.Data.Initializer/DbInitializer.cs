namespace UniversitySystem.Data.Initializer
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Models;

    public static class DbInitializer
    {
        public static void UseDatabaseInitialize(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<UniversitySystemDbContext>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<Student>>();

                Task.Run(async () => await Initialize(dbContext, userManager)).Wait();
            }
        }

        private static async Task Initialize(UniversitySystemDbContext dbContext, UserManager<Student> userManager)
        {
            await dbContext.Database.MigrateAsync();

            //Database has been seeded
            if (dbContext.Users.Any())
            {
                return;
            }

            var students = await SeedUser(userManager);
            await SeedCoursesWithStudent(dbContext, students);
        }

        private static async Task<List<Student>> SeedUser(UserManager<Student> userManager)
        {
            List<Student> students = new List<Student>();
            int countOfTheStudentToBeAdd = 100;

            for (int i = 0; i < countOfTheStudentToBeAdd; i++)
            {
                var email = string.Format("test{0}@test.com", i);
                var password = string.Format("P@ssword{0}", i);

                var student = new Student();
                student.UserName = email;
                student.Email = email;

                var result = await userManager.CreateAsync(student, password);

                if (result.Succeeded)
                {
                    students.Add(student);
                }
            }

            return students;
        }

        private static async Task SeedCoursesWithStudent(UniversitySystemDbContext context, List<Student> stundets)
        {
            var countOfEmptyCourse = 50;
            var countOfCourseWithStudent = 50;

            int studentMaxScore = 100;

            Random random = new Random();

            for (var i = 0; i < countOfEmptyCourse; i++)
            {
                var course = new Course();
                course.Score = random.Next(1, 99);
                course.Name = string.Format("TestCourse{0}", i);

                context.Courses.Add(course);
            }

            for (var i = 50; i < countOfCourseWithStudent + 50; i++)
            {
                var course = new Course();
                course.Score = random.Next(1, 99);
                course.Name = string.Format("TestCourse{0}", i);

                var countOfStudentInCourse = random.Next(1, 5);
                var validStudents = stundets.Where(x => x.Courses.Sum(c => c.Course.Score) < studentMaxScore).Take(countOfCourseWithStudent);

                foreach (var student in validStudents)
                {
                    var studentCourse = new StudentCourse()
                    {
                        Course = course,
                        StudentId = student.Id,
                        Student = student
                    };

                    student.Courses.Add(studentCourse);
                    course.Students.Add(studentCourse);
                }

                context.Courses.Add(course);
            }

            await context.SaveChangesAsync();
        }
    }
}
