using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineExam.API.Data;
using OnlineExam.API.Models;

namespace OnlineExam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class ExamTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExamTypeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Add(string name, string description)
        {
            var type = new ExamType { Name = name, Description = description };
            _context.ExamTypes.Add(type);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Sınav türü başarıyla eklendi!", id = type.Id });
        }
    }
}