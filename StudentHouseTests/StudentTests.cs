using System;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using StudentHouse.Models;
using StudentHouse.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace StudentHouseTests
{
    public class StudentTests
    {
        [Fact]
        public void CallingAddStudentMethod_ReturnsTrue_WithValidObject()
        {
            // ARRANGE
            Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();
            var controller = new AccountController(studentMock.Object, null, null);

            studentMock.Setup(s => s.Students).Returns(new List<Student>
            {
                new Student { StudentId = 1, Firstname = "testFirstname1", Lastname = "testLastname1", PhoneNumber = "0612345678", Email = "testemail1@test.com" },
                new Student { StudentId = 2, Firstname = "testFirstname2", Lastname = "testLastname2", PhoneNumber = "0612345678", Email = "testemail2@test.com" }
            }.AsQueryable());

            // ACT
            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Student>;

            // ASSERT
            Assert.Equal(controller.studentRepository.Students, model, Comparer.Get<Student>((s1, s2) => s1.Firstname == s2.Firstname));
        }

    }
}
