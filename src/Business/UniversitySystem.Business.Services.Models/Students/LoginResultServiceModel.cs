namespace UniversitySystem.Business.Services.Models.Students
{
    public class LoginResultServiceModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool IsLoginSuccessful { get; set; }
        public string Token { get; set; }
    }
}
