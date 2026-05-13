using Microsoft.AspNetCore.Mvc;
using OnlineExam.API.Data;
using OnlineExam.API.Models;
using Microsoft.EntityFrameworkCore;

namespace OnlineExam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize] // 401 hatasını önlemek için kapalı tutuyoruz
    public class StudentAnswerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentAnswerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostAnswer([FromBody] StudentAnswerDto dto)
        {
            try
            {
                // Gelen verinin doğruluğunu kontrol ediyoruz
                if (dto.StudentExamId <= 0 || dto.QuestionId <= 0 || dto.SelectedOptionId <= 0)
                {
                    return BadRequest(new { message = "Eksik veya hatalı veri gönderildi.", data = dto });
                }

                var answer = new StudentAnswer
                {
                    StudentExamId = dto.StudentExamId,
                    QuestionId = dto.QuestionId,
                    SelectedOptionId = dto.SelectedOptionId
                };

                _context.StudentAnswers.Add(answer);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Cevap kaydedildi." });
            }
            catch (Exception ex)
            {
                // Hata mesajını en ince detayına kadar döner
                return StatusCode(500, new
                {
                    message = "Veritabanı kayıt hatası!",
                    error = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }
    }

    public class StudentAnswerDto
    {
        public int StudentExamId { get; set; }
        public int QuestionId { get; set; }
        public int SelectedOptionId { get; set; }
    }
}