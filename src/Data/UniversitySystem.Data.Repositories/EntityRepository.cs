namespace UniversitySystem.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;

    public class EntityRepository<T> : IRepository<T> where T : class
    {
        private readonly UniversitySystemDbContext universitySystemDbContext;

        public EntityRepository(UniversitySystemDbContext universitySystemDbContext)
        {
            this.universitySystemDbContext = universitySystemDbContext ?? throw new ArgumentNullException(nameof(universitySystemDbContext));
        }

        public int Add(T entity)
        {
            this.universitySystemDbContext.Set<T>().Add(entity);

            return this.universitySystemDbContext.SaveChanges();
        }

        public async Task<int> AddAsync(T entity)
        {
            await this.universitySystemDbContext.AddAsync(entity);

            return await this.universitySystemDbContext.SaveChangesAsync();
        }

        public int AddRange(params T[] entities)
        {
            this.universitySystemDbContext.Set<T>().AddRange(entities);

            return this.universitySystemDbContext.SaveChanges();
        }

        public async Task<int> AddRangeAsync(params T[] entities)
        {
            await this.universitySystemDbContext.Set<T>().AddRangeAsync(entities);

            return await this.universitySystemDbContext.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
            return this.universitySystemDbContext.Set<T>();
        }

        public bool Delete(T entity)
        {
            this.universitySystemDbContext.Remove(entity);

            int affectedRow = this.universitySystemDbContext.SaveChanges();

            return affectedRow > 0;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            this.universitySystemDbContext.Set<T>().Remove(entity);

            int affectedRow = await this.universitySystemDbContext.SaveChangesAsync();

            return affectedRow > 0;
        }

        public void DeleteRange(params T[] entities)
        {
            this.universitySystemDbContext.Set<T>().RemoveRange(entities);
        }

        public async Task DeleteRangeAsync(params T[] entities)
        {
            this.universitySystemDbContext.Set<T>().RemoveRange(entities);

            await this.universitySystemDbContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            this.universitySystemDbContext.Set<T>().Update(entity);

            this.universitySystemDbContext.SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            this.universitySystemDbContext.Set<T>().Update(entity);

            await this.universitySystemDbContext.SaveChangesAsync();
        }

        public void UpdateRange(params T[] entities)
        {
            this.universitySystemDbContext.Set<T>().UpdateRange(entities);

            this.universitySystemDbContext.SaveChanges();
        }

        public async Task UpdateRangeAsync(params T[] entities)
        {
            this.universitySystemDbContext.Set<T>().UpdateRange(entities);

            await this.universitySystemDbContext.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return this.universitySystemDbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.universitySystemDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.universitySystemDbContext.Dispose();
        }
    }
}
