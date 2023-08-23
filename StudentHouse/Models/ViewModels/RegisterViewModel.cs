using System.ComponentModel.DataAnnotations;

namespace StudentHouse.Models.ViewModels
{
    public class RegisterViewModel
    {
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
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
