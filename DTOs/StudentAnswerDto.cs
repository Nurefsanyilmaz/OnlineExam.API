namespace OnlineExam.API.DTOs
{
    public class StudentAnswerDto
    {
        public int QuestionId { get; set; }
        public int SelectedOptionId { get; set; }
        public int StudentExamId { get; set; }
    }
}