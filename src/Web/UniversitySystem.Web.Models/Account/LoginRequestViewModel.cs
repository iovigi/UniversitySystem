namespace UniversitySystem.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class LoginRequestViewModel
    {


        [Required]
        [MinLength(GlobalAccountConstants.MinEmailLength)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
