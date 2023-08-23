using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentHouse.Models
{
    public class Meal
    {
        [Key]
        public int MealId { get; set; }
        [Required(ErrorMessage = "Naam is vereist.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Omschrijving is vereist.")]
        public string Description { get; set; }
        public string CookName { get; set; }
        public string CookEmail { get; set; }
        [Required(ErrorMessage = "Maximaal aantal gasten is vereist.")]
        public int MaxGuests { get; set; }
        [Required(ErrorMessage = "Prijs is vereist.")]
        public double Price { get; set; }
        public DateTime DateTime { get; set; }
        public virtual ICollection<StudentMeal> Guests { get; set; }
    }
}
