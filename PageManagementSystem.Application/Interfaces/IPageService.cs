using PageManagementSystem.Core.Entities;

namespace PageManagementSystem.Application.Interfaces
{
    public interface IPageService
    {
        Task<IEnumerable<Page>> GetAllPagesAsync();
        Task<Page> GetPageByIdAsync(int id);
        Task AddPageAsync(Page page);
        Task UpdatePageAsync(Page page);
        Task DeletePageAsync(int id);
    }
}
