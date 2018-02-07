namespace UniversitySystem.Data.Repositories.Contracts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Common;

    public interface IRepository<T> : IDisposable, IDependency where T : class
    {
        /// <summary>
        /// Get all entries of generic type of repository
        /// </summary>
        /// <returns>Return all entries in data store</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Add entry in data store
        /// </summary>
        /// <param name="entity">Entry, which will be added</param>
        void Add(T entity);

        /// <summary>
        /// Add entry asynchronously in data store
        /// </summary>
        /// <param name="entity">Entry, which will be add</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Add entries in data store
        /// </summary>
        /// <param name="entity">Entries, which will be add</param>
        void AddRange(params T[] entities);

        /// <summary>
        /// Add entry asynchronously in data store
        /// </summary>
        /// <param name="entity">Entries, which will be add</param>
        /// <returns>Return task for operation add</returns> 
        Task AddRangeAsync(params T[] entities);

        /// <summary>
        /// Delete entry from data store
        /// </summary>
        /// <param name="entity">Entry which will be delete</param>
        /// <returns>Return true if entry is deleted successfully, otherwise return false</returns>
        bool Delete(T entity);

        /// <summary>
        /// Delete entry asynchronously from data store
        /// </summary>
        /// <param name="entity">Entry which will be delete</param>
        /// <returns>Return task for operation delete with parameter bool, which is true if entry is deleted successfully, otherwise return false</returns>
        Task<bool> DeleteAsync(T entity);

        /// <summary>
        /// Delete entry by id from data store
        /// </summary>
        /// <param name="id">Id of the entry</param>
        /// <returns>Return true is delete entry successfully, otherwise false</returns>
        bool Delete(object id);

        /// <summary>
        /// Delete entry asynchronously by id from data store
        /// </summary>
        /// <param name="id">Id of the entry</param>
        /// <returns>Return task for operation delete by id with parameter bool, which true is delete entry successfully, otherwise false</returns>
        Task<bool> DeleteAsync(object id);

        /// <summary>
        /// Delete range of entries from data store
        /// </summary>
        /// <param name="entities">entries, which will be delete</param>
        void DeleteRange(params T[] entities);

        /// <summary>
        /// Delete range asynchronously of entries from data store
        /// </summary>
        /// <param name="entities">entries, which will be delete</param>
        /// <returns>Return task for operation range delete</returns>
        Task DeleteRangeAsync(params T[] entities);

        /// <summary>
        /// Return entry by id from data store
        /// </summary>
        /// <param name="id">Id of the entry</param>
        /// <returns>Return entry, if exist entry in  data store with this id, otherwise return null</returns>
        T GetById(object id);

        /// <summary>
        /// Return entry asynchronously by id from data store
        /// </summary>
        /// <param name="id">Id of the entry</param>
        /// <returns>Return task for operation get entry by id with generic parameter, which represent entry, if exist entry in  data store with this id, otherwise parameter is null</returns>
        Task<T> GetByIdAsync(object id);

        /// <summary>
        /// Update entry
        /// </summary>
        /// <param name="entity">Entry, which will be update</param>
        void Update(T entity);

        /// <summary>
        /// Update entry asynchronously
        /// </summary>
        /// <param name="entity">Entry, which will be update</param>
        /// <returns>Return task for operation update</returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Update range of entries
        /// </summary>
        /// <param name="entities">Entries, which will be update</param>
        void UpdateRange(params T[] entities);

        /// <summary>
        /// Update range of entries asynchronously
        /// </summary>
        /// <param name="entities">Entries, which will be update</param>
        /// <returns>Return task for operation range update</returns>
        Task UpdateRangeAsync(params T[] entities);

        /// <summary>
        /// Flush all info, which is stored in memory(cached) to be persist in data store
        /// </summary>
        /// <returns>Return affected rows</returns>
        int SaveChanges();

        /// <summary>
        /// Flush all info asynchronously, which is stored in memory(cached) to be persist in data store
        /// </summary>
        /// <returns>Return task for operation save changes with parameter int, which represend affected rows</returns>
        Task<int> SaveChangesAsync();
    }
}
