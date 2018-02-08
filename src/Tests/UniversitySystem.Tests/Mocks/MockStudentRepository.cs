namespace UniversitySystem.Tests.Mocks
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Models;
    using Data.Repositories.Contracts;

    internal class MockStudentRepository : IRepository<Student>
    {
        private readonly List<Student> students = new List<Student>();

        public MockStudentRepository()
        {
            students.AddRange(new Student[]
            {
                new Student()
                {
                    Id="1",
                    Email ="test@test.com",
                    PasswordHash="123",
                    UserName="test@test.com"
                },
                new Student()
                {
                    Id="2",
                    Email ="test2@test.com",
                    PasswordHash="123",
                    UserName="test@test.com"
                },
                new Student()
                {
                    Id="3",
                    Email ="test3@test.com",
                    PasswordHash="123",
                    UserName="test3@test.com"
                },
                new Student()
                {
                    Id="4",
                    Email ="test4@test.com",
                    PasswordHash="123",
                    UserName="test4@test.com"
                }
            });

            for (int i = 10; i < 100; i++)
            {
                this.students.Add(new Student()
                {
                    Id = i.ToString(),
                    Email = string.Format("test{0}@test.com", i),
                    PasswordHash = "123" + i,
                    UserName = string.Format("test{0}@test.com", i)
                });
            }
        }

        public void Add(Student entity)
        {
            this.students.Add(entity);
        }

        public async Task AddAsync(Student entity)
        {
            this.students.Add(entity);
        }

        public void AddRange(params Student[] entities)
        {
            this.students.AddRange(entities);
        }

        public async Task AddRangeAsync(params Student[] entities)
        {
            this.students.AddRange(entities);
        }

        public bool Delete(Student entity)
        {
            return this.students.Remove(entity);
        }

        public bool Delete(object id)
        {
            return this.students.RemoveAll(s => s.Id == id.ToString()) > 0;
        }

        public async Task<bool> DeleteAsync(Student entity)
        {
            return this.students.Remove(entity);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            return this.students.RemoveAll(s => s.Id == id.ToString()) > 0;
        }

        public void DeleteRange(params Student[] entities)
        {
            if (entities == null)
            {
                return;
            }

            this.students.RemoveAll(s => entities.Any(e => e.Id == s.Id));
        }

        public async Task DeleteRangeAsync(params Student[] entities)
        {
            if (entities == null)
            {
                return;
            }

            this.students.RemoveAll(s => entities.Any(e => e.Id == s.Id));
        }

        public void Dispose()
        {
        }

        public IQueryable<Student> GetAll()
        {
            return this.students.AsQueryable();
        }

        public Student GetById(object id)
        {
            return this.students.FirstOrDefault(s => s.Id == id.ToString());
        }

        public async Task<Student> GetByIdAsync(object id)
        {
            return this.students.FirstOrDefault(s => s.Id == id.ToString());
        }

        public int SaveChanges()
        {
            return 0;
        }

        public async Task<int> SaveChangesAsync()
        {
            return 0;
        }

        public void Update(Student entity)
        {
            var index = this.students.FindIndex(s => s.Id == entity.Id);

            if(index < 0)
            {
                return;
            }

            this.students.RemoveAt(index);
            this.students.Insert(index, entity);
        }

        public async Task UpdateAsync(Student entity)
        {
            var index = this.students.FindIndex(s => s.Id == entity.Id);

            if (index < 0)
            {
                return;
            }

            this.students.RemoveAt(index);
            this.students.Insert(index, entity);
        }

        public void UpdateRange(params Student[] entities)
        {
            foreach(var entity in entities)
            {
                var index = this.students.FindIndex(s => s.Id == entity.Id);

                if (index < 0)
                {
                    return;
                }

                this.students.RemoveAt(index);
                this.students.Insert(index, entity);
            }
        }

        public async Task UpdateRangeAsync(params Student[] entities)
        {
            foreach (var entity in entities)
            {
                var index = this.students.FindIndex(s => s.Id == entity.Id);

                if (index < 0)
                {
                    return;
                }

                this.students.RemoveAt(index);
                this.students.Insert(index, entity);
            }
        }
    }
}
