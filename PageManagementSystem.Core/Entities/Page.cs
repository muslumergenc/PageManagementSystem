namespace PageManagementSystem.Core.Entities
{
    public class Page
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? SeoMeta { get; set; }
        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; } 
        public string? SeoKeywords { get; set; }
        public ICollection<PageContent>? Contents { get; set; } = new List<PageContent>();
        public ICollection<PageData>? PageData { get; set; } = new List<PageData>();
    }
}
