namespace UniversitySystem.Tests.Web
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    using AutoMapper;
    using Xunit;

    using Mocks;
    using UniversitySystem.Web.Controllers;


    public class CourseControllerTest
    {
        private CourseController courseController;

        public CourseControllerTest()
        {
            MapperInit.Init();
            this.courseController = new CourseController(new MockCourseService(), new MockUserService(), Mapper.Instance);
        }

        [Fact]
        public void InitShouldThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CourseController(null, null, null));
        }

        [Fact]
        public async void CourseShouldBeRegisterSuccessfull()
        {
            var result = await this.courseController.RegisterCourse(1);

            Assert.IsType<OkObjectResult>(result);
            var data = ((dynamic)(result as OkObjectResult).Value).IsSuccessfull;

            Assert.True(data);
        }

        [Fact]
        public async void CourseShouldBeRegisterUnsuccessfull()
        {
            var result = await this.courseController.RegisterCourse(2);

            Assert.IsType<OkObjectResult>(result);
            var data = ((dynamic)(result as OkObjectResult).Value).IsSuccessfull;

            Assert.False(data);
        }

        [Fact]
        public async void CourseShouldBeUnRegisterSuccessfull()
        {
            var result = await this.courseController.RegisterCourse(1);

            Assert.IsType<OkObjectResult>(result);
            var data = ((dynamic)(result as OkObjectResult).Value).IsSuccessfull;

            Assert.True(data);
        }

        [Fact]
        public async void CourseShouldBeUnRegisterUnsuccessfull()
        {
            var result = await this.courseController.RegisterCourse(2);

            Assert.IsType<OkObjectResult>(result);
            var data = ((dynamic)(result as OkObjectResult).Value).IsSuccessfull;

            Assert.False(data);
        }

        [Fact]
        public void GetCourseListShouldSuccessfull()
        {
            var result = this.courseController.GetCourseList();

            Assert.IsType<ViewResult>(result);
        }
    }
}
