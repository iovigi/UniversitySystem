namespace UniversitySystem.Web.Controllers
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using AutoMapper;

    using Business.Services.Contracts;
    using Common;
    using Models.Account;
    using Infrastructure.Filters;

    [CheckIfUserIsLogin(GlobalCourseConstants.ControllerName, GlobalCourseConstants.GetCourseListActionName)]
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public AccountController(IUserService userService, IMapper mapper)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Action for Login
        /// </summary>
        /// <returns>View for Login</returns>
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        /// <summary>
        /// Asyncronosly action method for login of the student
        /// </summary>
        /// <param name="request">Model with email and password for student, who want to login</param>
        /// <returns>Return task with result of login.</returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestViewModel request)
        {
            if ((!this.ModelState.IsValid) || request == null)
            {
                return View();
            }

            var result = await this.userService.LoginAsync(request.Email, request.Password);
            var resultModel = this.mapper.Map<LoginResultViewModel>(result);

            if (!result.IsLoginSuccessful)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");

                return View(request);
            }

            return this.RedirectToAction(GlobalCourseConstants.GetCourseListActionName, GlobalCourseConstants.ControllerName);
        }

        /// <summary>
        /// Action for register
        /// </summary>
        /// <returns>View for register</returns>
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        /// <summary>
        /// Asyncronosly action method for register of the student
        /// </summary>
        /// <param name="request">Model with email and password for student, who want to register</param>
        /// <returns>Return task with result of register.</returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestViewModel request)
        {
            if ((!this.ModelState.IsValid) || request == null)
            {
                return this.BadRequest();
            }

            var result = await this.userService.RegisterAsync(request.Email, request.Password);
            var resultModel = this.mapper.Map<RegisterResultViewModel>(result);

            if (!result.IsRegisteredSuccessful)
            {
                ModelState.AddModelError(string.Empty, "Invalid register attempt.");

                return View(request);
            }

            return this.RedirectToAction(GlobalCourseConstants.GetCourseListActionName, GlobalCourseConstants.ControllerName);
        }
    }
}