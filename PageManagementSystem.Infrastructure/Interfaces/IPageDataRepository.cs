using PageManagementSystem.Core.Entities;

namespace PageManagementSystem.Infrastructure.Interfaces
{
    public interface IPageDataRepository
    {
        Task<PageData?[]> GetAllAsync(); 
        Task<PageData[]> GetAllByPageIdAsync(int pageId); // Belirli bir sayfaya ait tüm içerik gruplarını getirir
        Task<PageData?> GetByIdAsync(int id); // İçerik grubunu ID'ye göre getirir
        Task AddAsync(PageData? pageData); // Yeni bir içerik grubu ekler
        Task UpdateAsync(PageData? pageData); // Mevcut bir içerik grubunu günceller
        Task DeleteAsync(int id); // İçerik grubunu siler
    }
}
