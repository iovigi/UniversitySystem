namespace UniversitySystem.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using AutoMapper;

    using Business.Services.Contracts;
    using Models.Account;

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
        /// Asyncronosly action method for login of the student
        /// </summary>
        /// <param name="request">Model with email and password for student, who want to login</param>
        /// <returns>Return task with result of login.</returns>
        public async Task<IActionResult> Login(LoginRequestViewModel request)
        {
            if (this.ModelState.IsValid || request == null)
            {
                return this.BadRequest();
            }

            var result = await this.userService.LoginAsync(request.Email, request.Password);
            var resultModel = this.mapper.Map<LoginResultViewModel>(result);

            return this.Ok(resultModel);
        }

        /// <summary>
        /// Asyncronosly action method for register of the student
        /// </summary>
        /// <param name="request">Model with email and password for student, who want to register</param>
        /// <returns>Return task with result of register.</returns>
        public async Task<IActionResult> Register(RegisterRequestViewModel request)
        {
            if (this.ModelState.IsValid || request == null)
            {
                return this.BadRequest();
            }

            var result = await this.userService.RegisterAsync(request.Email, request.Password);
            var resultModel = this.mapper.Map<RegisterResultViewModel>(result);

            return this.Ok(resultModel);
        }
    }
}