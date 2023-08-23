using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentHouse.Models;
using StudentHouse.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace StudentHouse.Controllers
{
    public class MealController : Controller
    {
        public readonly IStudentRepository studentRepository;
        public readonly IMealRepository mealRepository;

        public MealController(IStudentRepository studentRepository, IMealRepository mealRepository)
        {
            this.studentRepository = studentRepository;
            this.mealRepository = mealRepository;
        }

        public IActionResult Index()
        {
            return View(mealRepository.Meals);
        }

        [Authorize]
        public IActionResult Detail(int mealId)
        {
            Meal meal = mealRepository.Meals.FirstOrDefault(m => m.MealId == mealId);
            ICollection<Student> students = new List<Student>();

            foreach (var mealStudent in meal.Guests)
            {
                students.Add(mealStudent.Student);
            }

            if (meal != null)
            {
                MealDetailModel mealDetailModel = new MealDetailModel
                {
                    Meal = meal,
                    Guests = students
                };
                return View(mealDetailModel);
            }
            else
            {
                return View("Index");
            }
        }

        [Authorize]
        public IActionResult Create(Meal meal)
        {
            Student student = studentRepository.Students.FirstOrDefault(s => s.Email == User.Identity.Name);
            meal.CookName = student.GetFullName();
            meal.CookEmail = student.Email;

            if (ModelState.IsValid)
            {
                if (mealRepository.AddMeal(meal))
                {
                    return Redirect("Index");
                }
            }

            return View();
        }

        [Authorize]
        public IActionResult Edit(int mealId)
        {
            Meal meal = mealRepository.Meals.Where(m => m.MealId == mealId).FirstOrDefault();

            if (meal != null && meal.Guests.Count() == 0)
            {
                return View(mealRepository.Meals.Where(m => m.MealId == mealId).FirstOrDefault());
            }

            return Redirect("Index");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(Meal meal)
        {
            if (ModelState.IsValid)
            {
                if (mealRepository.EditMeal(meal))
                {
                    return Redirect("Index");
                }
            }

            return View();
        }

        [Authorize]
        public IActionResult Delete(int mealId)
        {
            Meal meal = mealRepository.Meals.Where(m => m.MealId == mealId).FirstOrDefault();

            if (mealRepository.DeleteMeal(mealId) == true)
            {
                return Redirect("Index");
            }
            else
            {
                return Redirect("Index");
            }
        }

        [Authorize]
        public IActionResult AddGuestToMeal(int mealId)
        {
            Student student = studentRepository.Students.FirstOrDefault(s => s.Email == User.Identity.Name);
            mealRepository.AddGuestToMeal(mealId, student);

            return Redirect("Index");
        }

        [Authorize]
        public IActionResult DeleteGuestFromMeal(int mealId)
        {
            Student student = studentRepository.Students.FirstOrDefault(s => s.Email == User.Identity.Name);
            mealRepository.DeleteGuestFromMeal(mealId, student);

            return Redirect("Index");
        }
    }
}