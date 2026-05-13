namespace OnlineExam.API.Models
{
    public class ExamType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Exam> Exams { get; set; }
    }
}