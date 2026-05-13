using Microsoft.EntityFrameworkCore;
using OnlineExam.API.Data;
using OnlineExam.API.Models;

namespace OnlineExam.API.Repositories
{
    public class OptionRepository
    {
        private readonly AppDbContext _context;

        public OptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Option>> GetAllAsync()
        {
            return await _context.Options.ToListAsync();
        }

        public async Task<Option> GetByIdAsync(int id)
        {
            return await _context.Options.FindAsync(id);
        }

        public async Task<Option> AddAsync(Option option)
        {
            await _context.Options.AddAsync(option);
            await _context.SaveChangesAsync();
            return option;
        }

        public async Task UpdateAsync(Option option)
        {
            _context.Options.Update(option);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Option option)
        {
            _context.Options.Remove(option);
            await _context.SaveChangesAsync();
        }
    }
}