namespace UniversitySystem.Data.Repositories.Contracts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Common;

    public interface IRepository<T> : IDisposable, IDependency where T : class
    {
        IQueryable<T> GetAll();

        void Add(T entity);

        Task AddAsync(T entity);

        void AddRange(params T[] entities);

        Task AddRangeAsync(params T[] entities);

        bool Delete(T entity);

        Task<bool> DeleteAsync(T entity);

        bool Delete(object id);

        Task<bool> DeleteAsync(object id);

        void DeleteRange(params T[] entities);

        Task DeleteRangeAsync(params T[] entities);

        T GetById(object id);

        Task<T> GetByIdAsync(object id);

        void Update(T entity);

        Task UpdateAsync(T entity);

        void UpdateRange(params T[] entities);

        Task UpdateRangeAsync(params T[] entities);

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
