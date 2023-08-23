using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHouse.Models
{
    public class EFMealRepository : IMealRepository
    {
        public ApplicationDbContext context;
        public EFMealRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Meal> Meals => context.Meals;

        public bool AddMeal(Meal meal)
        {
            foreach (Meal m in context.Meals)
            {
                if (m.Name == meal.Name) { return false; }

                if (m.DateTime.Day == meal.DateTime.Day && m.DateTime.Month == meal.DateTime.Month && m.DateTime.Year == meal.DateTime.Year) { return false; }
            }
            context.Meals.Add(meal);
            return context.SaveChanges() > 0;
        }

        public bool EditMeal(Meal meal)
        {
            if (context.Meals.SingleOrDefault(m => m.MealId == meal.MealId) == null) { return false; }

            Meal EditMeal = context.Meals.SingleOrDefault(m => m.MealId == meal.MealId);

            bool sameDateAsMealsInContext = true;
            foreach (Meal m in context.Meals)
            {
                if (m.DateTime.Day == meal.DateTime.Day && m.DateTime.Month == meal.DateTime.Month && m.DateTime.Year == meal.DateTime.Year)
                {
                    sameDateAsMealsInContext = false;
                }
            }

            if (sameDateAsMealsInContext || (meal.DateTime.Day == EditMeal.DateTime.Day && meal.DateTime.Month == EditMeal.DateTime.Month && meal.DateTime.Year == EditMeal.DateTime.Year))
            {
                EditMeal.Name = meal.Name;
                EditMeal.Description = meal.Description;
                EditMeal.DateTime = meal.DateTime;
                EditMeal.MaxGuests = meal.MaxGuests;
                EditMeal.Price = meal.Price;

                return context.SaveChanges() > 0;
            } else
            {
                return false;
            }
        }

        public bool DeleteMeal(int mealId)
        {
            if (context.Meals.SingleOrDefault(m => m.MealId == mealId) == null) { return false; }
            Meal DeleteMeal = context.Meals.SingleOrDefault(m => m.MealId == mealId);
            context.Meals.Remove(DeleteMeal);
            return context.SaveChanges() > 0;
        }

        public bool AddGuestToMeal(int mealId, Student student)
        {
            Meal meal = Meals.FirstOrDefault(m => m.MealId == mealId);

            if (meal != null)
            {
                StudentMeal studentMeal = new StudentMeal { Student = student, Meal = meal };

                meal.Guests.Add(studentMeal);
                return context.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteGuestFromMeal(int mealId, Student student)
        {
            Meal meal = Meals.FirstOrDefault(m => m.MealId == mealId);

            var studentMeal = meal.Guests.FirstOrDefault(s => s.StudentId == student.StudentId);

            if (meal != null && studentMeal != null)
            {
                meal.Guests.Remove(studentMeal);
                return context.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }
    }
}
