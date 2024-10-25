using System.ComponentModel.DataAnnotations;

namespace PageManagementSystem.Core.Entities
{
    public class PageContent
    {
        public int Id { get; set; }

        [Required]
        public int PageId { get; set; } // İlgili sayfa ID’si
        public Page Page { get; set; }

        [Required]
        public string ContentType { get; set; } // İçerik türü: Text, Image, Video, vb.

        [Required]
        public string ContentValue { get; set; } // İçeriğin kendisi (metin, resim yolu, video bağlantısı vb.)

        public int Order { get; set; } // İçerik sırası
    }
}
