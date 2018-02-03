namespace UniversitySystem.Business.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Models.Courses;
    using Data.Repositories.Contracts;
    using Data.Models;
    using UniversitySystem.Common;

    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> courseRepository;
        private readonly IRepository<Student> studentRepository;

        public CourseService(IRepository<Course> courseRepository, IRepository<Student> studentRepository)
        {
            this.courseRepository = courseRepository;
            this.studentRepository = studentRepository;
        }

        public async Task AddAsync(string name, int score)
        {
            var course = new Course()
            {
                Name = name,
                Score = score
            };

            await this.courseRepository.AddAsync(course);
        }

        public async Task<bool> DeleteAsync(int courseId)
        {
            var isEmpty = await this.IsCourseEmptyAsync(courseId);

            if (!isEmpty)
            {
                return false;
            }

            return await this.courseRepository.DeleteAsync(courseId);
        }

        public IQueryable<CourseServiceModel> GetAll()
        {
            return this.courseRepository.GetAll()
                .Select(c => new CourseServiceModel(c.Id, c.Name, c.Score, c.Students.Count));
        }

        public async Task<CourseServiceModel> GetAsync(int courseId)
        {
            var course = await this.courseRepository.GetByIdAsync(courseId);

            if (course == null)
            {
                return null;
            }

            return new CourseServiceModel(course.Id, course.Name, course.Score, course.Students.Count);
        }

        public CourseListsByStudentServiceModel GetCourseListsByStudent(string studentId)
        {
            var courseListsByStudent = new CourseListsByStudentServiceModel(studentId);

            var courses = this.courseRepository.GetAll()
                .Select(x => new { x.Id, x.Name, x.Score, x.Students })
                .ToList();

            foreach (var course in courses)
            {
                var courseServiceModel = new CourseServiceModel(course.Id, course.Name, course.Score, course.Students.Count);

                if (course.Students.Any(x => x.StudentId == studentId))
                {
                    courseListsByStudent.RegisteredCourses.Add(courseServiceModel);
                }
                else
                {
                    courseListsByStudent.NotRegisteredCourses.Add(courseServiceModel);
                }
            }

            return courseListsByStudent;
        }

        public async Task<bool> IsCourseEmptyAsync(int courseId)
        {
            var course = await this.GetAsync(courseId);

            if (course != null)
            {
                return course.CountOfStudent == 0;
            }

            return true;
        }

        public async Task<bool> RegisterStudentAsync(int courseId, string studentId)
        {
            var course = await this.courseRepository.GetByIdAsync(courseId);
            var student = await this.studentRepository.GetByIdAsync(studentId);

            if (course == null || student == null)
            {
                return false;
            }

            if(student.Courses.Sum(x=> x.Course.Score) >= GlobalStudentConstants.MaxScore)
            {
                return false;
            }

            if (course.Students.Any(x => x.StudentId == studentId))
            {
                return false;
            }

            course.Students.Add(
                new StudentCourse()
                {
                    StudentId = studentId,
                    Student = student,
                    CourseId = courseId,
                    Course = course
                });

            await this.courseRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UnRegisterStudentAsync(int courseId, string studentId)
        {
            var course = await this.courseRepository.GetByIdAsync(courseId);
            var student = await this.studentRepository.GetByIdAsync(studentId);

            if (course == null || student == null)
            {
                return false;
            }

            var studentCourse = course.Students.FirstOrDefault(x => x.StudentId == studentId);

            if (studentCourse == null)
            {
                return false;
            }

            course.Students.Remove(studentCourse);

            await this.courseRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateCourseAsync(int courseId, string name, int score)
        {
            var course = await this.courseRepository.GetByIdAsync(courseId);

            if(course.Students.Count > 0)
            {
                return false;
            }

            course.Name = name;
            course.Score = score;

            await this.courseRepository.SaveChangesAsync();

            return true;
        }
    }
}
