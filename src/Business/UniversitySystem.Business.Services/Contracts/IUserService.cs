namespace UniversitySystem.Business.Services.Contracts
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using UniversitySystem.Business.Services.Models.Students;
    using UniversitySystem.Common;

    public  interface IUserService : IDependency
    {
        /// <summary>
        /// Asynchronously register the user by email and password.
        /// </summary>
        /// <param name="email">Email of the user</param>
        /// <param name="password">Password of the user</param>
        /// <returns>Return task with parameter register result with token and IsRegisteredSuccessful true, if user register successfully, otherwise return Login result with IsRegisteredSuccessful false</returns>
        Task<RegisterResultServiceModel> RegisterAsync(string email, string password);

        /// <summary>
        /// Asynchronously Login the user by email and password.
        /// </summary>
        /// <param name="email">Email of the user</param>
        /// <param name="password">Password of the user</param>
        /// <returns>Return Task with parameter login result with token and IsLoginSuccessful true, if user login successfully, otherwise return Login result with IsLoginSuccessful false</returns>
        Task<LoginResultServiceModel> LoginAsync(string email, string password);

        /// <summary>
        /// Get user id by claim principal
        /// </summary>
        /// <param name="user">Claim principal</param>
        /// <returns>Return user id</returns>
        string GetUserId(ClaimsPrincipal user);
    }
}
