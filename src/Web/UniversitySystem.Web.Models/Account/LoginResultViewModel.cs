namespace UniversitySystem.Web.Models.Account
{
    using Business.Services.Models.Students;
    using Common.Mapping;

    public class LoginResultViewModel : IMapFrom<LoginResultServiceModel>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool IsLoginSuccesful { get; set; }
    }
}
