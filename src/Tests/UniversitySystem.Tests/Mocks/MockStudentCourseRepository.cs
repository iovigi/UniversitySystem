namespace UniversitySystem.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Data.Models;
    using Data.Repositories.Contracts;

    public class MockStudentCourseRepository : IRepository<StudentCourse>
    {
        public int Add(StudentCourse entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(StudentCourse entity)
        {
            throw new NotImplementedException();
        }

        public int AddRange(params StudentCourse[] entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddRangeAsync(params StudentCourse[] entities)
        {
            throw new NotImplementedException();
        }

        public bool Delete(StudentCourse entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(StudentCourse entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(params StudentCourse[] entities)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRangeAsync(params StudentCourse[] entities)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IQueryable<StudentCourse> GetAll()
        {
            return new List<StudentCourse>()
            {
                new StudentCourse()
                {
                    StudentId ="1",
                    CourseId =0
                }
            }.AsQueryable();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(StudentCourse entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(StudentCourse entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(params StudentCourse[] entities)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRangeAsync(params StudentCourse[] entities)
        {
            throw new NotImplementedException();
        }
    }
}
