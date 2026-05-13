namespace OnlineExam.API.Models
{
    public class StudentAnswer
    {
        public int Id { get; set; }
        public int StudentExamId { get; set; }
        public int QuestionId { get; set; }
        public int SelectedOptionId { get; set; }

        public StudentExam StudentExam { get; set; }
        public Question Question { get; set; }
        public Option SelectedOption { get; set; }
    }
}