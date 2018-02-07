namespace UniversitySystem.Web.Models.Course
{
    public class ResultViewModel
    {
        public ResultViewModel(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; set; }
    }
}
