namespace UniversitySystem.Business.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Common;
    using Models.Courses;

    public interface ICourseService : IDependency
    {
        /// <summary>
        /// Get all courses which is store in data storage
        /// </summary>
        /// <returns>return all available courses</returns>
        Task<IEnumerable<CourseServiceModel>> GetAllAsync();

        /// <summary>
        /// Add Course to data store. If name is null or white space, throw ArgumentException. If score is less than zero throw ArgumentException.
        /// </summary>
        /// <param name="name">name of course</param>
        /// <param name="score">score of course</param>
        Task AddAsync(string name, int score);

        /// <summary>
        /// Check is there are registered student to the course by course id
        /// </summary>
        /// <param name="courseId">id of the course</param>
        /// <returns>If course is empty, return true, otherwise return false</returns>
        Task<bool> IsCourseEmptyAsync(int courseId);

        /// <summary>
        /// Get course by id
        /// </summary>
        /// <param name="courseId">Id of the course</param>
        /// <returns>If there is course with this Id, return it, otherwise return null</returns>
        Task<CourseServiceModel> GetAsync(int courseId);

        /// <summary>
        /// Delete course by course id, if course is empty
        /// </summary>
        /// <param name="courseId">id of the course</param>
        /// <returns>return true if course is delete succefully, otherwise return false</returns>
        Task<bool> DeleteAsync(int courseId);

        /// <summary>
        /// Update course by course id, if course is empty
        /// </summary>
        /// <param name="courseId">id of the course</param>
        /// <returns>return true if course is updated successfully, otherwise return false</returns>
        Task<bool> UpdateCourseAsync(int courseId, string name, int score);

        /// <summary>
        /// Register student in course, if student isn't registered, course and student isn't null. 
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="studentId"></param>
        /// <returns>return true if student register to course successfully, otherwise return false</returns>
        Task<bool> RegisterStudentAsync(int courseId, string studentId);

        /// <summary>
        /// Remove student from course, if student is registered, course and student isn't null. 
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="studentId"></param>
        /// <returns>return true if student unregister to course successfully, otherwise return false</returns>
        Task<bool> UnRegisterStudentAsync(int courseId, string studentId);

        /// <summary>
        /// Return lists with registered and non registred courses for student
        /// </summary>
        /// <param name="studentId">Id of the student</param>
        /// <returns>Model with both lists, registered and non registred courses</returns>
        CourseListsByStudentServiceModel GetCourseListsByStudent(string studentId);
    }
}
