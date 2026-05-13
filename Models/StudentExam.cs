namespace OnlineExam.API.Models
{
    public class StudentExam
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public int ExamId { get; set; }
        public int Score { get; set; }
        public string Status { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public Exam Exam { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}