using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineExam.API.DTOs;
using OnlineExam.API.Models;
using OnlineExam.API.Repositories;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OnlineExam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionController : ControllerBase
    {
        private readonly QuestionRepository _questionRepository;

        public QuestionController(QuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [HttpGet("ByExam/{examId}")]
        public async Task<IActionResult> GetByExamId(int examId)
        {
            var questions = await _questionRepository.GetQuestionsByExamIdAsync(examId);

            var result = questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                ExamId = q.ExamId,
                Text = q.Text,
                PointValue = q.PointValue,
                Options = q.Options.Select(o => new OptionDto
                {
                    Id = o.Id, 
                    Text = o.Text,
                    IsCorrect = o.IsCorrect,
                    QuestionId = q.Id
                }).ToList()
            }).ToList();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var questions = await _questionRepository.GetAllAsync();
            return Ok(questions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            if (question == null) return NotFound(new { message = "Soru bulunamadı!" });
            return Ok(question);
        }

        [HttpPost]
        public async Task<IActionResult> Add(QuestionDto dto)
        {
            var question = new Question
            {
                ExamId = dto.ExamId,
                Text = dto.Text,
                PointValue = dto.PointValue,
                Options = dto.Options.Select(o => new Option
                {
                    Text = o.Text,
                    IsCorrect = o.IsCorrect
                }).ToList()
            };

            await _questionRepository.AddAsync(question);
            return Ok(new { message = "Soru başarıyla eklendi!", id = question.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, QuestionDto dto)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            if (question == null) return NotFound(new { message = "Soru bulunamadı!" });

            question.ExamId = dto.ExamId;
            question.Text = dto.Text;
            question.PointValue = dto.PointValue;

            await _questionRepository.UpdateAsync(question);
            return Ok(new { message = "Soru başarıyla güncellendi!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            if (question == null) return NotFound(new { message = "Soru bulunamadı!" });

            await _questionRepository.DeleteAsync(question);
            return Ok(new { message = "Soru başarıyla silindi!" });
        }
    }
}