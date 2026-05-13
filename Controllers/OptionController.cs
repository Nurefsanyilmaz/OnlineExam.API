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
    public class OptionController : ControllerBase
    {
        private readonly OptionRepository _optionRepository;

        public OptionController(OptionRepository optionRepository)
        {
            _optionRepository = optionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _optionRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var option = await _optionRepository.GetByIdAsync(id);
            if (option == null) return NotFound(new { message = "Şık bulunamadı!" });
            return Ok(option);
        }

        [HttpPost]
        public async Task<IActionResult> Add(OptionDto dto)
        {
            var option = new Option
            {
                QuestionId = dto.QuestionId,
                Text = dto.Text,
                IsCorrect = dto.IsCorrect
            };

            await _optionRepository.AddAsync(option);
            return Ok(new { message = "Şık başarıyla eklendi!", id = option.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OptionDto dto)
        {
            var option = await _optionRepository.GetByIdAsync(id);
            if (option == null) return NotFound(new { message = "Şık bulunamadı!" });

            option.QuestionId = dto.QuestionId;
            option.Text = dto.Text;
            option.IsCorrect = dto.IsCorrect;

            await _optionRepository.UpdateAsync(option);
            return Ok(new { message = "Şık başarıyla güncellendi!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var option = await _optionRepository.GetByIdAsync(id);
            if (option == null) return NotFound(new { message = "Şık bulunamadı!" });

            await _optionRepository.DeleteAsync(option);
            return Ok(new { message = "Şık başarıyla silindi!" });
        }
    }
}