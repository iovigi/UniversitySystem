namespace UniversitySystem.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class RegisterRequestViewModel
    {
        [Required]
        [MinLength(5)]
        [RegularExpression(GlobalStudentConstants.EmailRegex)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
