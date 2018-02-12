namespace UniversitySystem.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class RegisterRequestViewModel
    {
        [Required]
        [MinLength(GlobalAccountConstants.MinEmailLength)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
