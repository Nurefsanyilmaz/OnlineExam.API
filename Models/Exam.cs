namespace OnlineExam.API.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public int ExamTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DurationInMinutes { get; set; }
        public int PassingScore { get; set; }
        public bool IsRandomOrder { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public DateTime StartDate { get; set; }

        public ExamType ExamType { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}