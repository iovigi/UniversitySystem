namespace UniversitySystem.Business.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Models.Courses;
    using Data.Repositories.Contracts;
    using Data.Models;

    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> courseRepository;
        private readonly IRepository<StudentCourse> studentCourseRepository;

        public CourseService(IRepository<Course> courseRepository,IRepository<StudentCourse> studentCourseRepository)
        {
            this.courseRepository = courseRepository;
            this.studentCourseRepository = studentCourseRepository;
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

            if(!isEmpty)
            {
                return false;
            }

            throw new NotImplementedException();
        }

        public Task<IQueryable<CourseServiceModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CourseServiceModel> GetAsync(int courseId)
        {
            throw new NotImplementedException();
        }

        public Task<CourseListsByStudentServiceModel> GetCourseListsByAsync(string studentId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsCourseEmptyAsync(int courseId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterStudentAsync(int courseId, string studentId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnRegisterStudentAsync(int courseId, string studentId)
        {
            throw new NotImplementedException();
        }

        public Task<CourseServiceModel> UpdateCourseAsync(int courseId, string name, int score)
        {
            throw new NotImplementedException();
        }
    }
}
