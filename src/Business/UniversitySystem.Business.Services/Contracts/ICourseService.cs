namespace UniversitySystem.Business.Services.Contracts
{
    using System.Threading.Tasks;
    using System.Linq;

    using Models.Courses;

    public interface ICourseService
    {
        IQueryable<CourseServiceModel> GetAll();

        Task AddAsync(string name, int score);

        Task<bool> IsCourseEmptyAsync(int courseId);

        Task<CourseServiceModel> GetAsync(int courseId);

        Task<bool> DeleteAsync(int courseId);

        Task<bool> UpdateCourseAsync(int courseId, string name, int score);

        Task<bool> RegisterStudentAsync(int courseId, string studentId);

        Task<bool> UnRegisterStudentAsync(int courseId, string studentId);

        CourseListsByStudentServiceModel GetCourseListsByStudent(string studentId);
    }
}
