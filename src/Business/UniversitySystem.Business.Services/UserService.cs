
namespace UniversitySystem.Business.Services
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Identity;

    using Contracts;
    using Data.Models;
    using System.Threading.Tasks;
    using Models.Students;

    public class UserService : IUserService
    {
        private readonly UserManager<Student> userManager;
        private readonly SignInManager<Student> signInManager;

        public UserService(UserManager<Student> userManager, SignInManager<Student> signInManager)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager)); ;
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager)); ;
        }

        public async Task<LoginResultServiceModel> LoginAsync(string email, string password)
        {
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            var result = await this.signInManager.PasswordSignInAsync(email, password, false, false);
            if (result.Succeeded)
            {
                var student = await this.userManager.FindByEmailAsync(email);

                return new LoginResultServiceModel()
                {
                    Id = student.Id,
                    Email = email,
                    IsLoginSuccessful = true

                };
            }

            return new LoginResultServiceModel();
        }

        public async Task<RegisterResultServiceModel> RegisterAsync(string email, string password)
        {
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            var user = new Student { UserName = email, Email = email };
            var result = await this.userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await this.signInManager.SignInAsync(user, isPersistent: false);

                return new RegisterResultServiceModel()
                {
                    Id = user.Id,
                    Email = email,
                    IsRegisteredSuccessful = true
                };
            }

            return new RegisterResultServiceModel()
            {
                ErrorMessages = result.Errors.Select(x => x.Description).ToArray()
            };
        }

        public string GetUserId(ClaimsPrincipal user)
        {
            return this.userManager.GetUserId(user);
        }
    }
}
