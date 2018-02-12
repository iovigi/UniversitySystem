namespace UniversitySystem.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Business.Services.Contracts;
    using Common;
    using Infrastructure.Filters;
    using Models.Course;

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
        /// Action for get lists of the course for the user (student)
        /// </summary>
        /// <returns>Return two lists of course, one with registered and one with non-registered for the user (student)</returns>
        [HttpGet]
        [CheckIfUserIsNotLogin(GlobalAccountConstants.ControllerName, GlobalAccountConstants.LoginActionName)]
        public IActionResult GetCourseList()
        {
            var studentId = this.userService.GetUserId(this.User);
            var model = this.courseService.GetCourseListsByStudent(studentId);
            var viewModel = this.mapper.Map<CourseListsByStudentViewModel>(model);

            return this.View(viewModel);
        }

        /// <summary>
        /// Asyncronosly action method for register in course for user 
        /// </summary>
        /// <param name="courseId">Course id</param>
        /// <returns>Return task with parameter action result, parameter is OkResult. In OkResult has result for success of register course for student</returns>
        [HttpPost]
        [CheckIfUserIsNotLoginApi(GlobalAccountConstants.ControllerName, GlobalAccountConstants.LoginActionName)]
        public async Task<IActionResult> RegisterCourse(int courseId)
        {
            var studentId = this.userService.GetUserId(this.User);
            var result = await this.courseService.RegisterStudentAsync(courseId, studentId);
            var viewModelResult = this.mapper.Map<RegisterToCourseViewModel>(result);

            return this.Ok(viewModelResult);
        }

        /// <summary>
        /// Asyncronosly action method for unregister in course for user 
        /// </summary>
        /// <param name="courseId">Course id</param>
        /// <returns>Return task with parameter action result, parameter is OkResult. In OkResult has result for success of unregister course for student</returns>
        [HttpPost]
        [CheckIfUserIsNotLoginApi(GlobalAccountConstants.ControllerName, GlobalAccountConstants.LoginActionName)]
        public async Task<IActionResult> UnRegisterCourse(int courseId)
        {
            var studentId = this.userService.GetUserId(this.User);
            var result = await this.courseService.UnRegisterStudentAsync(courseId, studentId);
            var viewModelResult = this.mapper.Map<UnRegisterToCourseViewModel>(result);

            return this.Ok(viewModelResult);
        }
    }
}