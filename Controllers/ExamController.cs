using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineExam.API.DTOs;
using OnlineExam.API.Models;
using OnlineExam.API.Repositories;

namespace OnlineExam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class ExamController : ControllerBase
    {
        private readonly ExamRepository _examRepository;

        public ExamController(ExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var exams = await _examRepository.GetAllAsync();
            return Ok(exams);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var exam = await _examRepository.GetByIdAsync(id);
            if (exam == null) return NotFound(new { message = "Sınav bulunamadı!" });
            return Ok(exam);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ExamDto dto)
        {
            var exam = new Exam
            {
                ExamTypeId = dto.ExamTypeId,
                Title = dto.Title,
                Description = dto.Description,
                DurationInMinutes = dto.DurationInMinutes,
                PassingScore = dto.PassingScore,
                IsRandomOrder = dto.IsRandomOrder,
                StartDate = dto.StartDate, 
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            await _examRepository.AddAsync(exam);
            return Ok(new { message = "Sınav başarıyla oluşturuldu!" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ExamDto dto)
        {
            var exam = await _examRepository.GetByIdAsync(id);
            if (exam == null) return NotFound(new { message = "Güncellenecek sınav bulunamadı!" });

            exam.Title = dto.Title;
            exam.Description = dto.Description;
            exam.DurationInMinutes = dto.DurationInMinutes;
            exam.PassingScore = dto.PassingScore;
            exam.IsRandomOrder = dto.IsRandomOrder;
            exam.ExamTypeId = dto.ExamTypeId;
            exam.StartDate = dto.StartDate; 

            await _examRepository.UpdateAsync(exam);
            return Ok(new { message = "Sınav başarıyla güncellendi!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exam = await _examRepository.GetByIdAsync(id);
            if (exam == null) return NotFound(new { message = "Silinecek sınav zaten mevcut değil!" });

            await _examRepository.DeleteAsync(id);
            return Ok(new { message = "Sınav başarıyla silindi!" });
        }
    }
}