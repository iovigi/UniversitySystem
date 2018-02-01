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
            this.universitySystemDbContext = universitySystemDbContext;
        }

        public void Add(T entity)
        {
            this.universitySystemDbContext.Set<T>().Add(entity);

            this.universitySystemDbContext.SaveChanges();
        }

        public async Task AddAsync(T entity)
        {
            await this.universitySystemDbContext.AddAsync(entity);
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

        public IQueryable<T> All()
        {
            return this.universitySystemDbContext.Set<T>().AsQueryable();
        }

        public void Delete(T entity)
        {
            this.universitySystemDbContext.Remove(entity);

            this.universitySystemDbContext.SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
            this.universitySystemDbContext.Set<T>().Remove(entity);

            await this.universitySystemDbContext.SaveChangesAsync();
        }

        public void Delete(object id)
        {
            var entity = this.GetById(id);

            this.Delete(entity);
        }

        public async Task DeleteAsync(object id)
        {
            var entity = await this.GetByIdAsync(id);

            this.universitySystemDbContext.Set<T>().Remove(entity);

            await this.universitySystemDbContext.SaveChangesAsync();
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

        public void Dispose()
        {
            this.universitySystemDbContext.Dispose();
        }
    }
}
