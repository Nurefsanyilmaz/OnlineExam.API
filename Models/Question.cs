namespace OnlineExam.API.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public string Text { get; set; }
        public int PointValue { get; set; }

        public Exam Exam { get; set; }
        public ICollection<Option> Options { get; set; }
    }
}