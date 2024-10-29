using Microsoft.EntityFrameworkCore;
using PageManagementSystem.Core.Entities;
using PageManagementSystem.Infrastructure.Data;
using PageManagementSystem.Infrastructure.Interfaces;
namespace PageManagementSystem.Infrastructure.Repositories
{
    public class PageRepository : IPageRepository
    {
        private readonly ApplicationDbContext _context;

        public PageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Page[]> GetAllAsync()
        {
            return await _context.Pages.OrderBy(x=>x.Order).ToArrayAsync();
        }

        public async Task<Page> GetByIdAsync(int id)
        {
            return await _context.Pages.FindAsync(id);
        }

        public async Task AddAsync(Page page)
        {
            await _context.Pages.AddAsync(page);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Page page)
        {
            _context.Pages.Update(page);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var page = await _context.Pages.FindAsync(id);
            if (page != null)
            {
                _context.Pages.Remove(page);
                await _context.SaveChangesAsync();
            }
        }
    }
}
