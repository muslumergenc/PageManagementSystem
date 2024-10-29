using Microsoft.EntityFrameworkCore;
using PageManagementSystem.Core.Entities;
using PageManagementSystem.Infrastructure.Data;
using PageManagementSystem.Infrastructure.Interfaces;

namespace PageManagementSystem.Infrastructure.Repositories
{
    public class PageDataRepository : IPageDataRepository
    {
        private readonly ApplicationDbContext _context;

        public PageDataRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PageData?[]> GetAllAsync()
        {
            return await _context.PageData
                .Include(pd => pd.Contents) // İçerik gruplarını içerikleriyle birlikte getir
                .ToArrayAsync();
        }

        public async Task<PageData[]> GetAllByPageIdAsync(int pageId)
        {
            return await _context.PageData
                .Include(pd => pd.Contents) // İçerik gruplarını içerikleriyle birlikte getir
                .Where(pd => pd.PageId == pageId)
                .ToArrayAsync();
        }

        public async Task<PageData> GetByIdAsync(int id)
        {
            return await _context.PageData
                .Include(pd => pd.Contents) // İçerikleriyle birlikte getir
                .FirstOrDefaultAsync(pd => pd.Id == id);
        }

        public async Task AddAsync(PageData pageData)
        {
            await _context.PageData.AddAsync(pageData);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PageData pageData)
        {
            _context.PageData.Update(pageData);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pageData = await _context.PageData.FindAsync(id);
            if (pageData != null)
            {
                _context.PageData.Remove(pageData);
                await _context.SaveChangesAsync();
            }
        }
    }
}
