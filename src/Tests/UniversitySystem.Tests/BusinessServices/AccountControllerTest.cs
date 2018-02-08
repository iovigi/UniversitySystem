namespace UniversitySystem.Tests.BusinessServices
{
    using Microsoft.AspNetCore.Mvc;

    using AutoMapper;
    using Xunit;

    using Tests.Mocks;
    using Web.Controllers;

    public class AccountControllerTest
    {
        private AccountController accountController;

        public AccountControllerTest()
        {
            MapperInit.Init();
            this.accountController = new AccountController(new MockUserService(), Mapper.Instance);
        }

        [Fact]
        public async void UserShouldLoginSuccessful()
        {
            var loginViewModel = new Web.Models.Account.LoginRequestViewModel()
            {
                Email = "test@test.bg",
                Password = "dsadas"
            };

            var result = await this.accountController.Login(loginViewModel);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void UserShouldLoginFail()
        {
            var result = await this.accountController.Login(null);

            Assert.IsType<BadRequestResult>(result);
        }

        public async void UserShouldRegisterSuccessful()
        {
            var registerViewModel = new Web.Models.Account.RegisterRequestViewModel()
            {
                Email = "test@test.bg",
                Password = "dsadas"
            };

            var result = await this.accountController.Register(registerViewModel);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
