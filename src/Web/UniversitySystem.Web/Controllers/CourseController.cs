namespace UniversitySystem.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using AutoMapper;

    using Business.Services.Contracts;
    using Common;
    using Infrastructure.Filters;
    using Models.Course;

    [SessionCheck(GlobalAccountConstants.ControllerName, GlobalAccountConstants.LoginActionName)]
    public class CourseController : Controller
    {
        private readonly ICourseService courseService;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public CourseController(ICourseService courseService, IUserService userService, IMapper mapper)
        {
            this.courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Asyncronosly action method for add of course
        /// </summary>
        /// <param name="request">Model with name and score of the course</param>
        /// <returns>Return task with parameter action result, if model is valid, parameter is OkResult, otherwise is BadRequest</returns>
        public async Task<IActionResult> Add(AddCourseViewModel request)
        {
            if(!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await this.courseService.AddAsync(request.Name, request.Score);

            return this.Ok();
        }


        /// <summary>
        /// Asyncronosly action method for update of course
        /// </summary>
        /// <param name="request">Model with name, score and id of the course</param>
        /// <returns>Return task with parameter action result, if model is valid, parameter is OkResult, otherwise is BadRequest. In OkResult has result for success of the update opeteration</returns>
        public async Task<IActionResult> Update(CourseViewModel request)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var isSuccess = await this.courseService.UpdateCourseAsync(request.Id, request.Name, request.Score);
            var resultModel = new ResultViewModel(isSuccess);

            return this.Ok(resultModel);
        }

        /// <summary>
        /// Asyncronosly action method for delete of course
        /// </summary>
        /// <param name="request">Course id</param>
        /// <returns>Return task with parameter action result, parameter is OkResult. In OkResult has result for success of the delete opeteration</returns>
        public async Task<IActionResult> Delete(int courseId)
        {
            var isSuccess = await this.courseService.DeleteAsync(courseId);
            var resultModel = new ResultViewModel(isSuccess);

            return this.Ok(resultModel);
        }

        /// <summary>
        /// Action for get lists of the course for the user (student)
        /// </summary>
        /// <returns>Return two lists of course, one with registered and one with non-registered for the user (student)</returns>
        public IActionResult GetCourseList()
        {
            var studentId = this.userService.GetUserId(this.User);
            var model = this.courseService.GetCourseListsByStudent(studentId);
            var viewModel = this.mapper.Map<CourseListsByStudentViewModel>(model);

            return this.Ok(viewModel);
        }

        /// <summary>
        /// Asyncronosly action method for register in course for user 
        /// </summary>
        /// <param name="courseId">Course id</param>
        /// <returns>Return task with parameter action result, parameter is OkResult. In OkResult has result for success of register course for student</returns>
        public async Task<IActionResult> RegisterCourse(int courseId)
        {
            var studentId = this.userService.GetUserId(this.User);
            var isSuccess = await this.courseService.RegisterStudentAsync(courseId, studentId);
            var resultModel = new ResultViewModel(isSuccess);

            return this.Ok(resultModel);
        }

        /// <summary>
        /// Asyncronosly action method for unregister in course for user 
        /// </summary>
        /// <param name="courseId">Course id</param>
        /// <returns>Return task with parameter action result, parameter is OkResult. In OkResult has result for success of unregister course for student</returns>
        public async Task<IActionResult> UnRegisterCourse(int courseId)
        {
            var studentId = this.userService.GetUserId(this.User);
            var isSuccess = await this.courseService.UnRegisterStudentAsync(courseId, studentId);
            var resultModel = new ResultViewModel(isSuccess);

            return this.Ok(resultModel);
        }
    }
}