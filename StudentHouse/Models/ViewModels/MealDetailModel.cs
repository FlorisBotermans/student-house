using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHouse.Models.ViewModels
{
    public class MealDetailModel
    {
        public Meal Meal { get; set; }
        public ICollection<Student> Guests { get; set; }
    }
}
