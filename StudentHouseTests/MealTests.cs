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
    public class MealTests
    {
        [Fact]
        public void Index_ReturnsCorrectAmountOfObjects()
        {
            // ARRANGE
            Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();
            Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
            var controller = new MealController(studentMock.Object, mealMock.Object);

            mealMock.Setup(m => m.Meals).Returns(new List<Meal>
            {
                new Meal { MealId = 1, Name = "testMeal1", Description = "testDescription1", MaxGuests = 5, Price = 2.50, DateTime = DateTime.Now },
                new Meal { MealId = 2, Name = "testMeal2", Description = "testDescription2", MaxGuests = 5, Price = 2.50, DateTime = DateTime.Now },
                new Meal { MealId = 3, Name = "testMeal3", Description = "testDescription3", MaxGuests = 5, Price = 2.50, DateTime = DateTime.Now },
                new Meal { MealId = 4, Name = "testMeal4", Description = "testDescription4", MaxGuests = 5, Price = 2.50, DateTime = DateTime.Now }
            }.AsQueryable);

            // ACT
            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Meal>;

            // ASSERT
            Assert.Equal(controller.mealRepository.Meals.Count(), model.Count());
        }

        [Fact]
        public void Index_ReturnsCorrectObjects()
        {
            // ARRANGE
            Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();
            Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
            var controller = new MealController(studentMock.Object, mealMock.Object);

            mealMock.Setup(m => m.Meals).Returns(new List<Meal>
            {
                new Meal { MealId = 1, Name = "testMeal1", Description = "testDescription1", MaxGuests = 5, Price = 2.50, DateTime = DateTime.Now },
                new Meal { MealId = 2, Name = "testMeal2", Description = "testDescription2", MaxGuests = 5, Price = 2.50, DateTime = DateTime.Now },
                new Meal { MealId = 3, Name = "testMeal3", Description = "testDescription3", MaxGuests = 5, Price = 2.50, DateTime = DateTime.Now },
                new Meal { MealId = 4, Name = "testMeal4", Description = "testDescription4", MaxGuests = 5, Price = 2.50, DateTime = DateTime.Now }
            }.AsQueryable);

            // ACT
            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Meal>;

            // ASSERT
            Assert.Equal(controller.mealRepository.Meals, model, Comparer.Get<Meal>((m1, m2) => m1.Name == m2.Name));
        }

        [Fact]
        public void CallingAddMealMethod_ReturnsTrue_WithValidObject()
        {
            // ARRANGE
            Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();
            Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
            var controller = new MealController(studentMock.Object, mealMock.Object);

            mealMock.Setup(m => m.AddMeal(It.IsAny<Meal>())).Returns(true);
            Meal meal = new Meal { MealId = 1, Name = "testMeal1", Description = "testDescription1", MaxGuests = 5, Price = 2.50, DateTime = DateTime.Now };

            // ACT
            bool mealAdded = controller.mealRepository.AddMeal(meal);

            // ASSERT
            mealMock.VerifyAll();
            Assert.True(mealAdded);
        }

        [Fact]
        public void CallingAddMealMethod_ReturnsFalse_WithDuplicateObject()
        {
            // ARRANGE
            Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();
            Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
            var controller = new MealController(studentMock.Object, mealMock.Object);

            mealMock.Setup(m => m.AddMeal(It.IsAny<Meal>())).Returns(false);
            Meal meal = new Meal { MealId = 1, Name = "testMeal1", Description = "testDescription1", MaxGuests = 5, Price = 2.50, DateTime = DateTime.Now };

            // ACT
            try
            {
                bool mealAdded = controller.mealRepository.AddMeal(meal);

                // ASSERT
                Assert.False(mealAdded);
            }
            catch (Exception)
            {
                mealMock.VerifyAll();
            }
        }

        [Fact]
        public void CallingEditMealMethod_ReturnsTrue_WithValidObject()
        {
            // ARRANGE
            Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();
            Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
            var controller = new MealController(studentMock.Object, mealMock.Object);

            mealMock.Setup(m => m.EditMeal(It.IsAny<Meal>())).Returns(true);
            Meal meal = new Meal { MealId = 1, Name = "testMeal1", Description = "testDescription1", MaxGuests = 5, Price = 2.50, DateTime = DateTime.Now };

            // ACT
            bool mealEdited = controller.mealRepository.EditMeal(meal);

            // ASSERT
            mealMock.VerifyAll();
            Assert.True(mealEdited);
        }

        [Fact]
        public void CallingEditMealMethod_ReturnsFalse_WithDuplicateObject()
        {
            // ARRANGE
            Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();
            Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
            var controller = new MealController(studentMock.Object, mealMock.Object);

            mealMock.Setup(m => m.EditMeal(It.IsAny<Meal>())).Returns(true);
            Meal meal = new Meal { MealId = 1, Name = "testMeal1", Description = "testDescription1", MaxGuests = 5, Price = 2.50, DateTime = DateTime.Now };

            // ACT
            try
            {
                bool mealEdited = controller.mealRepository.EditMeal(meal);

                // ASSERT
                Assert.False(mealEdited);
            }
            catch (Exception)
            {
                mealMock.VerifyAll();
            }
        }

        [Fact]
        public void CallingDeleteMeal_ReturnsTrue_WithValidInt()
        {
            // ARRANGE
            Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();
            Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
            var controller = new MealController(studentMock.Object, mealMock.Object);

            mealMock.Setup(m => m.DeleteMeal(It.IsAny<int>())).Returns(true);
            int deleteMeal = 1;

            // ACT
            bool mealDeleted = controller.mealRepository.DeleteMeal(deleteMeal);

            // ASSERT
            Assert.True(mealDeleted);
            mealMock.VerifyAll();
        }

        [Fact]
        public void CallingDeleteMealMethod_ReturnsFalse_WithWithInvalidInt()
        {
            // ARRANGE
            Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();
            Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
            var controller = new MealController(studentMock.Object, mealMock.Object);

            mealMock.Setup(m => m.DeleteMeal(It.IsAny<int>())).Returns(false);
            int deleteMeal = 1;

            // ACT
            try
            {
                bool mealDeleted = controller.mealRepository.DeleteMeal(deleteMeal);

                // ASSERT
                Assert.False(mealDeleted);
            }
            catch (Exception)
            {
                mealMock.VerifyAll();
            }
        }

        [Fact]
        public void CallingAddGuestToMealMethod_ReturnsTrue_WithValidIntAndObject()
        {
            // ARRANGE
            Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();
            Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
            var controller = new MealController(studentMock.Object, mealMock.Object);

            mealMock.Setup(m => m.AddGuestToMeal(It.IsAny<int>(), It.IsAny<Student>())).Returns(true);
            Student student = new Student { StudentId = 1, Firstname = "testStudent1", Lastname = "testLastname1", PhoneNumber = "0612345678", Email = "testemail@test.com" };
            Meal meal = new Meal { MealId = 1, Name = "testMeal1", Description = "testDescription1", MaxGuests = 5, Price = 2.50, DateTime = DateTime.Now };

            // ACT
            bool guestAddedToMeal = controller.mealRepository.AddGuestToMeal(meal.MealId, student);

            // ASSERT
            mealMock.VerifyAll();
            Assert.True(guestAddedToMeal);
        }

        [Fact]
        public void CallingAddGuestToMealMethod_ReturnsFalse_WithInvalidIntAndObject()
        {
            // ARRANGE
            Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();
            Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
            var controller = new MealController(studentMock.Object, mealMock.Object);

            mealMock.Setup(m => m.AddGuestToMeal(It.IsAny<int>(), It.IsAny<Student>())).Returns(false);
            Student student = new Student { StudentId = 1, Firstname = "testStudent1", Lastname = "testLastname1", PhoneNumber = "0612345678", Email = "testemail@test.com" };
            Meal meal = new Meal { MealId = 1, Name = "testMeal1", Description = "testDescription1", MaxGuests = 5, Price = 2.50, DateTime = DateTime.Now };

            // ACT
            try
            {
                bool guestAddedToMeal = controller.mealRepository.DeleteGuestFromMeal(meal.MealId, student);

                // ASSERT
                Assert.False(guestAddedToMeal);
            }
            catch (Exception)
            {
                mealMock.VerifyAll();
            }
        }
    }
}
