namespace UniversitySystem.Web.Models.Course
{
    public class ResultViewModel
    {
        public ResultViewModel(bool IsSucessfull)
        {
            this.IsSucessfull = IsSucessfull;
        }

        public bool IsSucessfull { get; set; }
    }
}
