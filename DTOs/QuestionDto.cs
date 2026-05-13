using System.Collections.Generic;

namespace OnlineExam.API.DTOs
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public string Text { get; set; }
        public int PointValue { get; set; }

        
        public List<OptionDto> Options { get; set; } = new List<OptionDto>();
    }
}