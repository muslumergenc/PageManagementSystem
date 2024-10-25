using System.ComponentModel.DataAnnotations;

namespace PageManagementSystem.Core.Entities
{
    public class PageData
    {
        public int Id { get; set; }

        [Required]
        public int PageId { get; set; } // Sayfa ID’si
        public Page Page { get; set; }

        [Required]
        public string DataType { get; set; } // Veri türü, örn: Ana içerik, alt içerik vb.

        public ICollection<PageContent> Contents { get; set; } = new List<PageContent>(); // Sayfaya ait içerik bileşenleri
    }
}
