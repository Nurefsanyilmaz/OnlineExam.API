namespace OnlineExam.API.DTOs
{
    public class ExamDto
    {
        public int ExamTypeId { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public int DurationInMinutes { get; set; }
        public int PassingScore { get; set; } 
        public bool IsRandomOrder { get; set; }
        public DateTime StartDate { get; set; }
    }
}