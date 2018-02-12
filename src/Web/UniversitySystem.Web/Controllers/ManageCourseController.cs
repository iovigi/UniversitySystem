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
    using Models.ManageCourse;

    public class ManageCourseController : Controller
    {
        private readonly ICourseService courseService;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public ManageCourseController(ICourseService courseService, IUserService userService, IMapper mapper)
        {
            this.courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Asyncronosly action method for add of course
        /// </summary>
        /// <param name="request">Model with name and score of the course</param>
        /// <returns>Return add result</returns>
        [HttpPost]
        [IgnoreAntiforgeryToken]
        [CheckIfUserIsNotLoginApi(GlobalAccountConstants.ControllerName, GlobalAccountConstants.LoginActionName)]
        public async Task<IActionResult> Add(AddCourseViewModel request)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var model = await this.courseService.AddAsync(request.Name, request.Score);

            if(model == null)
            {
                return this.Ok(new AddCourseViewModel());
            }

            request.Id = model.Id;
            request.IsSucessfull = true;

            return this.Ok(request);
        }

        /// <summary>
        /// Actin method for update of course
        /// </summary>
        ///<param name="courseId">Course id</param>
        /// <returns>Return view for update</returns>
        [HttpGet]
        [CheckIfUserIsNotLogin(GlobalAccountConstants.ControllerName, GlobalAccountConstants.LoginActionName)]
        public async Task<IActionResult> Update(int courseId)
        {
            var course = await this.courseService.GetAsync(courseId);
            var courseViewModel = this.mapper.Map<CourseViewModel>(course);

            return this.View(courseViewModel);
        }

        /// <summary>
        /// Asyncronosly action method for update of course
        /// </summary>
        /// <param name="request">Model with name, score and id of the course</param>
        /// <returns>Return task with parameter action result, if model is valid, redirect to get all action, otherwise is View for update with error.</returns>
        [HttpPost]
        [CheckIfUserIsNotLogin(GlobalAccountConstants.ControllerName, GlobalAccountConstants.LoginActionName)]
        public async Task<IActionResult> Update(CourseViewModel request)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(request);
            }

            var isSuccess = await this.courseService.UpdateCourseAsync(request.Id, request.Name, request.Score);
            var resultModel = new ResultViewModel(isSuccess);

            return this.RedirectToAction(GlobalManageCourseConstants.GetAllActionName);
        }

        /// <summary>
        /// Asyncronosly action method for delete of course
        /// </summary>
        /// <param name="request">Course id</param>
        /// <returns>Return task with parameter action result, parameter is OkResult. In OkResult has result for success of the delete opeteration</returns>
        [HttpPost]
        [IgnoreAntiforgeryToken]
        [CheckIfUserIsNotLoginApi(GlobalAccountConstants.ControllerName, GlobalAccountConstants.LoginActionName)]
        public async Task<IActionResult> Delete(int courseId)
        {
            var isSuccess = await this.courseService.DeleteAsync(courseId);
            var resultModel = new ResultViewModel(isSuccess);

            return this.Ok(resultModel);
        }

        /// <summary>
        /// Action to get all course
        /// </summary>
        /// <returns>Return all course</returns>
        [HttpGet]
        [CheckIfUserIsNotLogin(GlobalAccountConstants.ControllerName, GlobalAccountConstants.LoginActionName)]
        public IActionResult GetAll()
        {
            var viewModel = this.courseService.GetAll().ProjectTo<CourseDetailViewModel>().ToList();

            return this.View(viewModel);
        }
    }
}