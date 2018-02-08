namespace UniversitySystem.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Data.Models;
    using Data.Repositories.Contracts;

    internal class MockCourseRepository : IRepository<Course>
    {
        private readonly List<Course> courses = new List<Course>();

        public MockCourseRepository()
        {
            var students = new List<Student>(new Student[]
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
                students.Add(new Student()
                {
                    Id = i.ToString(),
                    Email = string.Format("test{0}@test.com", i),
                    PasswordHash = "123" + i,
                    UserName = string.Format("test{0}@test.com", i)
                });
            }

            for (int i = 0; i < 80; i++)
            {
                var course = new Course()
                {
                    Id = i,
                    Name = "testCourse" + 1,
                    Score = 100 - (i * 10),
                };

                for (int j = 0; j < i; j++)
                {
                    var studentCourse = new StudentCourse()
                    {
                        Student = students[j],
                        StudentId = students[j].Id,
                        Course = course,
                        CourseId = course.Id
                    };

                    course.Students.Add(studentCourse);
                    students[j].Courses.Add(studentCourse);
                }

                courses.Add(course);
            }
        }

        public void Add(Course entity)
        {
            this.courses.Add(entity);
        }

        public async Task AddAsync(Course entity)
        {
            this.courses.Add(entity);
        }

        public void AddRange(params Course[] entities)
        {
            this.courses.AddRange(entities);
        }

        public async Task AddRangeAsync(params Course[] entities)
        {
            this.courses.AddRange(entities);
        }

        public bool Delete(Course entity)
        {
            return this.courses.Remove(entity);
        }

        public bool Delete(object id)
        {
            return this.courses.RemoveAll(s => s.Id == (int)id) > 0;
        }

        public async Task<bool> DeleteAsync(Course entity)
        {
            return this.courses.Remove(entity);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            return this.courses.RemoveAll(c => c.Id == (int)id) > 0;
        }

        public void DeleteRange(params Course[] entities)
        {
            if (entities == null)
            {
                return;
            }

            this.courses.RemoveAll(c => entities.Any(e => e.Id == c.Id));
        }

        public async Task DeleteRangeAsync(params Course[] entities)
        {
            if (entities == null)
            {
                return;
            }

            this.courses.RemoveAll(c => entities.Any(x => x.Id == c.Id));
        }

        public void Dispose()
        {
        }

        public IQueryable<Course> GetAll()
        {
            return this.courses.AsQueryable();
        }

        public Course GetById(object id)
        {
            return this.courses.FirstOrDefault(c => c.Id == (int)id);
        }

        public async Task<Course> GetByIdAsync(object id)
        {
            return this.courses.FirstOrDefault(c => c.Id == (int)id);
        }

        public int SaveChanges()
        {
            return 0;
        }

        public async Task<int> SaveChangesAsync()
        {
            return 0;
        }

        public void Update(Course entity)
        {
            var index = this.courses.FindIndex(c => c.Id == entity.Id);

            if (index < 0)
            {
                return;
            }

            this.courses.RemoveAt(index);
            this.courses.Insert(index, entity);
        }

        public async Task UpdateAsync(Course entity)
        {
            var index = this.courses.FindIndex(c => c.Id == entity.Id);

            if (index < 0)
            {
                return;
            }

            this.courses.RemoveAt(index);
            this.courses.Insert(index, entity);
        }

        public void UpdateRange(params Course[] entities)
        {
            foreach (var entity in entities)
            {
                var index = this.courses.FindIndex(c => c.Id == entity.Id);

                if (index < 0)
                {
                    return;
                }

                this.courses.RemoveAt(index);
                this.courses.Insert(index, entity);
            }
        }

        public async Task UpdateRangeAsync(params Course[] entities)
        {
            foreach (var entity in entities)
            {
                var index = this.courses.FindIndex(c => c.Id == entity.Id);

                if (index < 0)
                {
                    return;
                }

                this.courses.RemoveAt(index);
                this.courses.Insert(index, entity);
            }
        }
    }
}
