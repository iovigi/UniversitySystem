namespace UniversitySystem.Business.Services.Contracts
{
    using System.Threading.Tasks;

    using UniversitySystem.Business.Services.Models.Students;
    using UniversitySystem.Common;

    public  interface IUserService : IDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<RegisterResultServiceModel> RegisterAsync(string email, string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<LoginResultServiceModel> LoginAsync(string email, string password);
    }
}
