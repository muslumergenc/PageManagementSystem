using Microsoft.EntityFrameworkCore;
using PageManagementSystem.Core.Entities;
using PageManagementSystem.Infrastructure.Data;
using PageManagementSystem.Infrastructure.Interfaces;

namespace PageManagementSystem.Infrastructure.Repositories
{
    public class PageContentRepository : IPageContentRepository
    {
        private readonly ApplicationDbContext _context;

        public PageContentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PageContent>> GetAllByPageDataIdAsync(int pageDataId)
        {
            return await _context.PageContents
                .Where(pc => pc.PageId == pageDataId)
                .ToListAsync();
        }

        public async Task<PageContent> GetByIdAsync(int id)
        {
            return await _context.PageContents.FindAsync(id);
        }

        public async Task AddAsync(PageContent pageContent)
        {
            await _context.PageContents.AddAsync(pageContent);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PageContent pageContent)
        {
            _context.PageContents.Update(pageContent);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pageContent = await _context.PageContents.FindAsync(id);
            if (pageContent != null)
            {
                _context.PageContents.Remove(pageContent);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PageContent>> GetAllContentAsync()
        {
            return await _context.PageContents.ToListAsync();
        }
    }
}
