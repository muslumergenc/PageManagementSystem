using System.ComponentModel.DataAnnotations;

namespace PageManagementSystem.Core.Entities
{
    public class PageContent
    {
        public int Id { get; set; }

        [Required]
        public int PageDataId { get; set; } // PageData ile ilişki kurar
        public PageData? PageData { get; set; } // Foreign Key: PageDataId üzerinden bağlanır

        [Required]
        public int PageId { get; set; } // Page ile ilişki kurar
        public Page? Page { get; set; } // Foreign Key: PageId üzerinden bağlanır

        [Required]
        public string ContentType { get; set; }
        public string ContentValue { get; set; }
        public int Order { get; set; }
    }
}
