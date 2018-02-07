namespace UniversitySystem.Business.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Common;
    using Models.Courses;

    public interface ICourseService : IDependency
    {
        /// <summary>
        /// Asynchronously get all courses which is store in data storage
        /// </summary>
        /// <returns>Return task with all available courses</returns>
        Task<IEnumerable<CourseServiceModel>> GetAllAsync();

        /// <summary>
        /// Asynchronously add Course to data store. If name is null or white space, throw ArgumentException. If score is less than zero throw ArgumentException.
        /// </summary>
        /// <param name="name">Name of course</param>
        /// <param name="score">Score of course</param>
        /// <returns>Return task for operation add</returns>
        Task AddAsync(string name, int score);

        /// <summary>
        /// Asynchronously check is there are registered student to the course by course id
        /// </summary>
        /// <param name="courseId">Id of the course</param>
        /// <returns>Return task with parameter bool, if course is empty, parameter is true, otherwise parameter is false</returns>
        Task<bool> IsCourseEmptyAsync(int courseId);

        /// <summary>
        /// Asynchronously get course by id
        /// </summary>
        /// <param name="courseId">Id of the course</param>
        /// <returns>Return task with parameter course service model, if there is course with this Id, otherwise parameter is null</returns>
        Task<CourseServiceModel> GetAsync(int courseId);

        /// <summary>
        /// Asynchronously delete course by course id, if course is empty
        /// </summary>
        /// <param name="courseId">Id of the course</param>
        /// <returns>Return task with parameter bool, parameter is true if course is delete succefully, otherwise parameter is false</returns>
        Task<bool> DeleteAsync(int courseId);

        /// <summary>
        /// Asynchronously update course by course id, if course is empty
        /// </summary>
        /// <param name="courseId">Id of the course</param>
        /// <returns>Return task with parameter bool, parameter is true if course is updated successfully, otherwise parameter is false</returns>
        Task<bool> UpdateCourseAsync(int courseId, string name, int score);

        /// <summary>
        /// Asynchronously register student in course, if student isn't registered, course and student isn't null. 
        /// </summary>
        /// <param name="courseId">Id of the course</param>
        /// <param name="studentId">Id of the student</param>
        /// <returns>Return task with parameter bool, parameter is true if student register to course successfully, otherwise parameter is false</returns>
        Task<bool> RegisterStudentAsync(int courseId, string studentId);

        /// <summary>
        /// Asynchronously remove student from course, if student is registered, course and student isn't null. 
        /// </summary>
        /// <param name="courseId">Id of the course</param>
        /// <param name="studentId">Id of the student</param>
        /// <returns>Return task with parameter bool, parameter is true if student unregister to course successfully, otherwise parameter is false</returns>
        Task<bool> UnRegisterStudentAsync(int courseId, string studentId);

        /// <summary>
        /// Return lists with registered and non registred courses for student
        /// </summary>
        /// <param name="studentId">Id of the student</param>
        /// <returns>Model with both lists, registered and non registred courses</returns>
        CourseListsByStudentServiceModel GetCourseListsByStudent(string studentId);
    }
}
