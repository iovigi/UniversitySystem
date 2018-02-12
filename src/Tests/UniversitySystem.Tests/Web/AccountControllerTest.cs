namespace UniversitySystem.Tests.Web
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    using AutoMapper;
    using Xunit;

    using Tests.Mocks;
    using UniversitySystem.Web.Controllers;

    public class AccountControllerTest
    {
        private AccountController accountController;

        public AccountControllerTest()
        {
            MapperInit.Init();
            this.accountController = new AccountController(new MockUserService(), Mapper.Instance);
        }

        [Fact]
        public void InitShouldThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new AccountController(null, null));
        }

        [Fact]
        public async void UserShouldLoginSuccessful()
        {
            var loginViewModel = new UniversitySystem.Web.Models.Account.LoginRequestViewModel()
            {
                Email = "test@test.bg",
                Password = "dsadas"
            };

            var result = await this.accountController.Login(loginViewModel);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async void UserShouldLoginFail()
        {
            var result = await this.accountController.Login(null);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void UserShouldRegisterSuccessful()
        {
            var registerViewModel = new UniversitySystem.Web.Models.Account.RegisterRequestViewModel()
            {
                Email = "test@test.bg",
                Password = "dsadas"
            };

            var result = await this.accountController.Register(registerViewModel);

            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
