namespace UniversitySystem.Data.Repositories.Contracts
{
    using System;
    using System.Threading.Tasks;
    using System.Linq;

    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> All();

        void Add(T entity);

        Task AddAsync(T entity);

        void AddRange(params T[] entities);

        Task AddRangeAsync(params T[] entities);

        void Delete(T entity);

        Task DeleteAsync(T entity);

        void Delete(object id);

        Task DeleteAsync(object id);

        void DeleteRange(params T[] entities);

        Task DeleteRangeAsync(params T[] entities);

        T GetById(object id);

        Task<T> GetByIdAsync(object id);

        void Update(T entity);

        Task UpdateAsync(T entity);

        void UpdateRange(params T[] entities);

        Task UpdateRangeAsync(params T[] entities);
    }
}
