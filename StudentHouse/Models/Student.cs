using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentHouse.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Voornaam is vereist.")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Achternaam is vereist.")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Telefoonnummer is vereist.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Dit is geen geldig telefoonnummer.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is vereist.")]
        [EmailAddress]
        public string Email { get; set; }
        public virtual ICollection<Meal> MealsCooked { get; set; }
        public virtual ICollection<StudentMeal> MealsAttend { get; set; }
        public string GetFullName()
        {
            return Firstname + " " + Lastname;
        }
    }
}
