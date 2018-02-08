namespace UniversitySystem.Tests.BusinessServices
{
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
            this.courseService = new CourseService(new MockCourseRepository(), new MockStudentRepository(), Mapper.Instance);
        }

        [Fact]
        public async void CourseShouldBeEmpty()
        {
            int courseIdEmpty = 0;
            var realValue = await this.courseService.IsCourseEmptyAsync(courseIdEmpty);

            Assert.True(realValue);
        }

        [Fact]
        public async void CourseShouldNotBeEmpty()
        {
            int courseIdNotEmpty = 1;
            var realValue = await this.courseService.IsCourseEmptyAsync(courseIdNotEmpty);

            Assert.False(realValue);
        }

        [Fact]
        public async void CourseShouldBeAdded()
        {
            string name = "CourseShouldBeAdded";
            int score = 60;

            await this.courseService.AddAsync(name, score);
            var course = this.courseService.GetAllAsync().FirstOrDefault(x => x.Name == name);

            Assert.NotNull(course);
        }

        [Fact]
        public async void GetCourseShouldBeNull()
        {
            var notExistingId = -10;
            var course = await this.courseService.GetAsync(notExistingId);

            Assert.Null(course);
        }

        [Fact]
        public async void GetCourseShouldBeNotNull()
        {
            var existingId = 1;
            var course = await this.courseService.GetAsync(existingId);

            Assert.NotNull(course);
        }

        [Fact]
        public async void CourseShouldBeDeleted()
        {
            var existingId = 0;
            var result = await this.courseService.DeleteAsync(existingId);

            Assert.True(result);
        }

        [Fact]
        public async void CourseShouldNotBeDeleted()
        {
            var existingId = 1;
            var result = await this.courseService.DeleteAsync(existingId);

            Assert.False(result);
        }

        [Fact]
        public async void CourseShouldBeUpdated()
        {
            var existingId = 0;
            var result = await this.courseService.UpdateCourseAsync(existingId, "test", 10);

            Assert.True(result);
        }

        [Fact]
        public async void CourseShouldBeRegister()
        {
            var existingCourseId = 0;
            var studentId = "1";
            var result = await this.courseService.RegisterStudentAsync(existingCourseId, studentId);

            Assert.True(result);
        }

        [Fact]
        public async void CourseShouldBeUnRegister()
        {
            var existingCourseId = 1;
            var studentId = "1";
            var result = await this.courseService.UnRegisterStudentAsync(existingCourseId, studentId);

            Assert.True(result);
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
