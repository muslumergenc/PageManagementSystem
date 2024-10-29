using PageManagementSystem.Application.Interfaces;
using PageManagementSystem.Core.Entities;
using PageManagementSystem.Infrastructure.Interfaces;

namespace PageManagementSystem.Application.Services
{
    public class PageContentService : IPageContentService
    {
        private readonly IPageContentRepository _pageContentRepository;

        public PageContentService(IPageContentRepository pageContentRepository)
        {
            _pageContentRepository = pageContentRepository;
        }

        public async Task<IEnumerable<PageContent>> GetAllContentAsync()
        {
            return await _pageContentRepository.GetAllContentAsync();
        }

        public async Task<IEnumerable<PageContent>> GetAllPageContentAsync(int pageDataId)
        {
            return await _pageContentRepository.GetAllByPageDataIdAsync(pageDataId);
        }

        public async Task<PageContent> GetPageContentByIdAsync(int id)
        {
            return await _pageContentRepository.GetByIdAsync(id);
        }

        public async Task AddPageContentAsync(PageContent pageContent)
        {
            await _pageContentRepository.AddAsync(pageContent);
        }

        public async Task UpdatePageContentAsync(PageContent pageContent)
        {
            await _pageContentRepository.UpdateAsync(pageContent);
        }

        public async Task DeletePageContentAsync(int id)
        {
            await _pageContentRepository.DeleteAsync(id);
        }
    }
}
