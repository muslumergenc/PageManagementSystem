using PageManagementSystem.Application.Interfaces;
using PageManagementSystem.Core.Entities;
using PageManagementSystem.Infrastructure.Interfaces;

namespace PageManagementSystem.Application.Services
{
    public class PageService : IPageService
    {
        private readonly IPageRepository _pageRepository;
        public PageService(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        public async Task<IEnumerable<Page>> GetAllPagesAsync()
        {
            return await _pageRepository.GetAllAsync();
        }

        public async Task<Page> GetPageByIdAsync(int id)
        {
            return await _pageRepository.GetByIdAsync(id);
        }

        public async Task AddPageAsync(Page page)
        {
            await _pageRepository.AddAsync(page);
        }

        public async Task UpdatePageAsync(Page page)
        {
            await _pageRepository.UpdateAsync(page);
        }

        public async Task DeletePageAsync(int id)
        {
            await _pageRepository.DeleteAsync(id);
        }
    }
}
