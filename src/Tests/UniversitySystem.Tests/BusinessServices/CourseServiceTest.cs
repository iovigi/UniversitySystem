namespace UniversitySystem.Tests.BusinessServices
{
    using System;
    using System.Linq;

    using AutoMapper;
    using Xunit;

    using Business.Services;
    using Mocks;

    public class CourseServiceTest
    {
        private CourseService courseService;

        private const string AssemblyUniversitySystem = "UniversitySystem";

        public CourseServiceTest()
        {
            MapperInit.Init();
            this.courseService = new CourseService(new MockCourseRepository(), new MockStudentRepository(), new MockStudentCourseRepository(), Mapper.Instance);
        }

        [Fact]
        public void InitShouldThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CourseService(null, null, null, null));
        }

        [Fact]
        public async void CourseShouldBeAdded()
        {
            string name = "CourseShouldBeAdded";
            int score = 60;

            await this.courseService.AddAsync(name, score);
            var course = this.courseService.GetAll().FirstOrDefault(x => x.Name == name);

            Assert.NotNull(course);
        }

        [Fact]
        public void GetCourseListsByStudentShouldHaveZeroRegisterCourse()
        {
            var expectedValue = 0;

            var studentId = "100";
            var result = this.courseService.GetCourseListsByStudent(studentId);

            Assert.Equal(result.RegisteredCourses.Count, expectedValue);
        }
    }
}
