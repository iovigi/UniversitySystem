namespace UniversitySystem.Business.Services.Models.Courses
{
    public class CourseServiceModel
    {
        public CourseServiceModel()
        {
        }

        public CourseServiceModel(int id, string name, int score, int countOfStudent)
        {
            this.Id = id;
            this.Name = name;
            this.Score = score;
            this.CountOfStudent = countOfStudent;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int CountOfStudent { get; set; }
    }
}
