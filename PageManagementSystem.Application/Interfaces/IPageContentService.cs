using PageManagementSystem.Core.Entities;

namespace PageManagementSystem.Application.Interfaces
{
    public interface IPageContentService
    {
        Task<IEnumerable<PageContent>> GetAllContentAsync(); // tüm içerikleri getirir
        Task<IEnumerable<PageContent>> GetAllPageContentAsync(int pageDataId); // Belirli bir içerik grubuna ait tüm içerikleri getirir
        Task<PageContent> GetPageContentByIdAsync(int id); // İçeriği ID'ye göre getirir
        Task AddPageContentAsync(PageContent pageContent); // Yeni bir içerik ekler
        Task UpdatePageContentAsync(PageContent pageContent); // Mevcut bir içeriği günceller
        Task DeletePageContentAsync(int id); // İçeriği siler
    }
}
