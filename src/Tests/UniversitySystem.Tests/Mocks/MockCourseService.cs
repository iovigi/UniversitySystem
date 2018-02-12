namespace UniversitySystem.Tests.Mocks
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Business.Services.Contracts;
    using Business.Services.Models.Courses;

    internal class MockCourseService : ICourseService
    {
        public async Task<CourseServiceModel> AddAsync(string name, int score)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int courseId)
        {
            if (courseId == 1)
            {
                return true;
            }

            return false;
        }

        public IQueryable<CourseServiceModel> GetAll()
        {
            return new List<CourseServiceModel>().AsQueryable();
        }

        public Task<CourseServiceModel> GetAsync(int courseId)
        {
            throw new System.NotImplementedException();
        }

        public CourseListsByStudentServiceModel GetCourseListsByStudent(string studentId)
        {
            return new CourseListsByStudentServiceModel()
            {
                StudentId = studentId
            };
        }

        public Task<bool> IsCourseEmptyAsync(int courseId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<RegisterToCourseServiceModel> RegisterStudentAsync(int courseId, string studentId)
        {
            if (courseId == 1 && studentId == "1")
            {
                return new RegisterToCourseServiceModel()
                {
                    IsSuccessfull = true,
                    CanRegisterMore = true
                };

            }

            return new RegisterToCourseServiceModel()
            {
                IsSuccessfull = false,
                CanRegisterMore = false
            };
        }

        public async Task<UnRegisterToCourseServiceModelcs> UnRegisterStudentAsync(int courseId, string studentId)
        {
            if (courseId == 1 && studentId == "1")
            {
                return new UnRegisterToCourseServiceModelcs()
                {
                    IsSuccessfull = true,
                    CanRegisterMore = true
                };
            }

            return new UnRegisterToCourseServiceModelcs()
            {
                IsSuccessfull = false,
                CanRegisterMore = false
            };
        }

        public async Task<bool> UpdateCourseAsync(int courseId, string name, int score)
        {
            if (courseId == 1)
            {
                return true;
            }

            return false;
        }
    }
}
