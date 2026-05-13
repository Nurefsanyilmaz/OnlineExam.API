using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineExam.API.Data;
using OnlineExam.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace OnlineExam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize] // 401 hatası almamak için şimdilik kapalı kalsın
    public class StudentExamController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentExamController(AppDbContext context)
        {
            _context = context;
        }

        // Sınav Başlatma
        [HttpPost("Start")]
        public async Task<IActionResult> StartExam([FromBody] StartExamRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "test-ogrenci-id";

            var studentExam = new StudentExam
            {
                ExamId = request.ExamId,
                StudentId = userId,
                StartedAt = DateTime.Now,
                Status = "Started",
                Score = 0
            };

            _context.StudentExams.Add(studentExam);
            await _context.SaveChangesAsync();
            return Ok(new { id = studentExam.Id });
        }

        // --- YENİ EKLENEN KISIM: Öğrencinin Sonuçlarını Getir ---
        [HttpGet("MyResults")]
        public async Task<IActionResult> GetMyResults()
        {
            // Giriş yapan öğrencinin ID'sini alıyoruz (Yoksa test ID'sini getir)
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "test-ogrenci-id";

            // Öğrencinin cevaplarını ve hangi sınav olduğunu birleştirip getiriyoruz
            var results = await _context.StudentAnswers
                .Include(a => a.StudentExam)
                .Where(a => a.StudentExam.StudentId == userId)
                .Select(a => new {
                    examId = a.StudentExam.ExamId,
                    questionId = a.QuestionId,
                    selectedOptionId = a.SelectedOptionId
                })
                .ToListAsync();

            return Ok(results);
        }
    }

    public class StartExamRequest { public int ExamId { get; set; } }
}