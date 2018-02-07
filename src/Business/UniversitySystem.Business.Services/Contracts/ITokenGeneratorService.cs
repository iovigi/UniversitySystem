namespace UniversitySystem.Business.Services.Contracts
{
    using Common;

    public interface ITokenGeneratorService : IDependency
    {
        /// <summary>
        /// Generate unique token
        /// </summary>
        /// <returns>Return generated token</returns>
        string GenerateToken();
    }
}
