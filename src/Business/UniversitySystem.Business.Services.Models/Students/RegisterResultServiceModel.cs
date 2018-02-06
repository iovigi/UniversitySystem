namespace UniversitySystem.Business.Services.Models.Students
{
    public class RegisterResultServiceModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool IsRegisteredSuccessful { get; set; }
        public string[] ErrorMessages { get; set; }
        public string Token { get; set; }
    }
}
