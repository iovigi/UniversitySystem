namespace UniversitySystem.Web.Models.Account
{
    using Business.Services.Models.Students;
    using Common.Mapping;

    public class RegisterResultViewModel : IMapFrom<RegisterResultServiceModel>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        
        public bool IsRegisterSuccesful { get; set; }

        public string[] ErrorMessages { get; set; }
    }
}
