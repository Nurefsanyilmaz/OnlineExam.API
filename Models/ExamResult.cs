namespace OnlineExam.API.Models
{
    public class ExamResult
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public int UserId { get; set; } 
        public int Score { get; set; } 
        public DateTime CompletedAt { get; set; }
        public Exam Exam { get; set; }
    }
}