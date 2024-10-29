using PageManagementSystem.Core.Entities;

namespace PageManagementSystem.Application.Interfaces
{
    public interface IPageDataService
    {
        Task<PageData[]> GetAllPageDataAsync();
        Task<PageData[]> GetAllPageDataAsync(int pageId); // Belirli bir sayfanın tüm içerik gruplarını getirir
        Task<PageData> GetPageDataByIdAsync(int id); // İçerik grubunu ID'ye göre getirir
        Task AddPageDataAsync(PageData pageData); // Yeni bir içerik grubu ekler
        Task UpdatePageDataAsync(PageData pageData); // Mevcut bir içerik grubunu günceller
        Task DeletePageDataAsync(int id); // İçerik grubunu siler
    }
}
