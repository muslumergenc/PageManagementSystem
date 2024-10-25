using PageManagementSystem.Application.Interfaces;
using PageManagementSystem.Core.Entities;
using PageManagementSystem.Infrastructure.Interfaces;

namespace PageManagementSystem.Application.Services
{
    public class PageDataService : IPageDataService
    {
        private readonly IPageDataRepository _pageDataRepository;

        public PageDataService(IPageDataRepository pageDataRepository)
        {
            _pageDataRepository = pageDataRepository;
        }

        public async Task<IEnumerable<PageData>> GetAllPageDataAsync(int pageId)
        {
            return await _pageDataRepository.GetAllByPageIdAsync(pageId);
        }

        public async Task<PageData> GetPageDataByIdAsync(int id)
        {
            return await _pageDataRepository.GetByIdAsync(id);
        }

        public async Task AddPageDataAsync(PageData pageData)
        {
            await _pageDataRepository.AddAsync(pageData);
        }

        public async Task UpdatePageDataAsync(PageData pageData)
        {
            await _pageDataRepository.UpdateAsync(pageData);
        }

        public async Task DeletePageDataAsync(int id)
        {
            await _pageDataRepository.DeleteAsync(id);
        }
    }
}
