namespace UniversitySystem.Tests.Mocks
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Business.Services.Contracts;
    using Business.Services.Models.Students;

    internal class MockUserService : IUserService
    {
        public string GetUserId(ClaimsPrincipal user)
        {
            return "1";
        }

        public async Task<LoginResultServiceModel> LoginAsync(string email, string password)
        {
            return new LoginResultServiceModel() { IsLoginSuccessful = true };
        }

        public async Task<RegisterResultServiceModel> RegisterAsync(string email, string password)
        {
            return new RegisterResultServiceModel() { IsRegisteredSuccessful = true };
        }
    }
}
