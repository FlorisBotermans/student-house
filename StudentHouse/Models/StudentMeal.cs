using System;
using System.ComponentModel.DataAnnotations;

namespace StudentHouse.Models
{
    public class StudentMeal
    {
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public int MealId { get; set; }
        public virtual Meal Meal { get; set; }
    }
}
