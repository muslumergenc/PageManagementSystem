using PageManagementSystem.Core.Entities;

namespace PageManagementSystem.Infrastructure.Interfaces
{
    public interface IPageRepository
    {
        Task<Page[]> GetAllAsync();
        Task<Page> GetByIdAsync(int id);
        Task AddAsync(Page page);
        Task UpdateAsync(Page page);
        Task DeleteAsync(int id);
    }
}
