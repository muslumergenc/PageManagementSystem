using PageManagementSystem.Core.Entities;

namespace PageManagementSystem.Infrastructure.Interfaces
{
    public interface IPageContentRepository
    {
        Task<IEnumerable<PageContent>> GetAllByPageDataIdAsync(int pageDataId); // Belirli bir içerik grubuna ait tüm içerikleri getirir
        Task<PageContent> GetByIdAsync(int id); // İçeriği ID'ye göre getirir
        Task AddAsync(PageContent pageContent); // Yeni bir içerik ekler
        Task UpdateAsync(PageContent pageContent); // Mevcut bir içeriği günceller
        Task DeleteAsync(int id); // İçeriği siler
        Task<IEnumerable<PageContent>> GetAllContentAsync();
    }
}
