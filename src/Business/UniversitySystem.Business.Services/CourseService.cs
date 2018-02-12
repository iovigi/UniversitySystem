namespace UniversitySystem.Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Common;
    using Contracts;
    using Data.Repositories.Contracts;
    using Data.Models;
    using Models.Courses;


    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> courseRepository;
        private readonly IRepository<Student> studentRepository;
        private readonly IRepository<StudentCourse> studentCourseRepository;
        private readonly IMapper mapper;

        public CourseService(IRepository<Course> courseRepository, IRepository<Student> studentRepository, IRepository<StudentCourse> studentCourseRepository, IMapper mapper)
        {
            this.courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            this.studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
            this.studentCourseRepository = studentCourseRepository ?? throw new ArgumentNullException(nameof(studentCourseRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
        }

        public async Task<CourseServiceModel> AddAsync(string name, int score)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid Name");
            }

            if (score < 0)
            {
                throw new ArgumentException("Invalid Score");
            }

            var course = new Course()
            {
                Name = name,
                Score = score
            };

            var resultCount = await this.courseRepository.AddAsync(course);

            if(resultCount == 0)
            {
                return null;
            }

            return this.mapper.Map<CourseServiceModel>(course);
        }

        public async Task<bool> DeleteAsync(int courseId)
        {
            var isEmpty = await this.IsCourseEmptyAsync(courseId);

            if (!isEmpty)
            {
                return false;
            }

            var course = await this.courseRepository.GetAll().FirstOrDefaultAsync(x => x.Id == courseId);

            return await this.courseRepository.DeleteAsync(course);
        }

        public IQueryable<CourseServiceModel> GetAll()
        {
            return this.courseRepository.GetAll().ProjectTo<CourseServiceModel>();
        }

        public async Task<CourseServiceModel> GetAsync(int courseId)
        {
            var course = await this.courseRepository.GetAll().Select(x => new { x.Id, x.Name, x.Score, x.Students.Count }).FirstOrDefaultAsync(x => x.Id == courseId);

            if (course == null)
            {
                return null;
            }

            return new CourseServiceModel(course.Id, course.Name, course.Score, course.Count);
        }

        public CourseListsByStudentServiceModel GetCourseListsByStudent(string studentId)
        {
            var courseListsByStudent = new CourseListsByStudentServiceModel(studentId);

            var courses = this.courseRepository.GetAll()
                .Select(x => new { x.Id, x.Name, x.Score, x.Students })
                .ToList();

            foreach (var course in courses)
            {
                var courseServiceModel = new CourseServiceModel(course.Id, course.Name, course.Score, course.Students.Count);

                if (course.Students.Any(x => x.StudentId == studentId))
                {
                    courseListsByStudent.RegisteredCourses.Add(courseServiceModel);
                }
                else
                {
                    courseListsByStudent.NotRegisteredCourses.Add(courseServiceModel);
                }
            }

            courseListsByStudent.CanRegisterMoreCourse = courseListsByStudent.RegisteredCourses.Sum(c => c.Score) < GlobalStudentConstants.MaxScore;

            return courseListsByStudent;
        }

        public async Task<bool> IsCourseEmptyAsync(int courseId)
        {
            var course = await this.GetAsync(courseId);

            if (course != null)
            {
                return course.CountOfStudent == 0;
            }

            return true;
        }

        public async Task<RegisterToCourseServiceModel> RegisterStudentAsync(int courseId, string studentId)
        {
            var course = await this.courseRepository.GetAll().Select(x => new { x.Id, x.Students, x.Score }).FirstOrDefaultAsync(x => x.Id == courseId);
            var student = await this.studentRepository.GetAll().Select(x => new { x.Id }).FirstOrDefaultAsync(x => x.Id == studentId);
            var studentCoure = await this.studentCourseRepository.GetAll().Where(x => x.StudentId == studentId).Select(x => x.Course).ToListAsync();

            if (course == null || student == null)
            {
                return new RegisterToCourseServiceModel();
            }

            var score = studentCoure.Sum(x => x.Score);

            if (score >= GlobalStudentConstants.MaxScore)
            {
                return new RegisterToCourseServiceModel();
            }

            if (course.Students.Any(x => x.StudentId == studentId))
            {
                return new RegisterToCourseServiceModel();
            }

            course.Students.Add(
                new StudentCourse()
                {
                    StudentId = studentId,
                    CourseId = courseId
                });

            await this.courseRepository.SaveChangesAsync();

            return new RegisterToCourseServiceModel()
            {
                IsSuccessfull = true,
                CanRegisterMore = (score + course.Score) < GlobalStudentConstants.MaxScore
            };
        }

        public async Task<UnRegisterToCourseServiceModelcs> UnRegisterStudentAsync(int courseId, string studentId)
        {
            var course = await this.courseRepository.GetAll().Select(x => new { x.Id, x.Students, x.Score }).FirstOrDefaultAsync(x => x.Id == courseId);
            var student = await this.studentRepository.GetAll().FirstOrDefaultAsync(x => x.Id == studentId);

            if (course == null || student == null)
            {
                return new UnRegisterToCourseServiceModelcs();
            }

            var studentCourse = course.Students.FirstOrDefault(x => x.StudentId == studentId);

            if (studentCourse == null)
            {
                return new UnRegisterToCourseServiceModelcs();
            }

            course.Students.Remove(studentCourse);

            await this.courseRepository.SaveChangesAsync();

            var score = await this.studentCourseRepository.GetAll().Where(x => x.StudentId == studentId).Select(x => x.Course.Score).SumAsync();

            return new UnRegisterToCourseServiceModelcs()
            {
                IsSuccessfull = true,
                CanRegisterMore = (score - course.Score) < GlobalStudentConstants.MaxScore
            };
        }

        public async Task<bool> UpdateCourseAsync(int courseId, string name, int score)
        {
            var course = await this.courseRepository.GetAll().FirstOrDefaultAsync(x => x.Id == courseId);

            if (course.Students.Count > 0)
            {
                return false;
            }

            course.Name = name;
            course.Score = score;

            await this.courseRepository.SaveChangesAsync();

            return true;
        }
    }
}
