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

        public void Add(T entity)
        {
            this.universitySystemDbContext.Set<T>().Add(entity);

            this.universitySystemDbContext.SaveChanges();
        }

        public async Task AddAsync(T entity)
        {
            await this.universitySystemDbContext.AddAsync(entity);
            await this.universitySystemDbContext.SaveChangesAsync();
        }

        public void AddRange(params T[] entities)
        {
            this.universitySystemDbContext.Set<T>().AddRange(entities);

            this.universitySystemDbContext.SaveChanges();
        }

        public async Task AddRangeAsync(params T[] entities)
        {
            await this.universitySystemDbContext.Set<T>().AddRangeAsync(entities);

            await this.universitySystemDbContext.SaveChangesAsync();
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

        public bool Delete(object id)
        {
            var entity = this.GetById(id);

            return this.Delete(entity);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var entity = await this.GetByIdAsync(id);

            return await DeleteAsync(entity);
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

        public T GetById(object id)
        {
            return this.universitySystemDbContext.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await this.universitySystemDbContext.Set<T>().FindAsync(id);
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
