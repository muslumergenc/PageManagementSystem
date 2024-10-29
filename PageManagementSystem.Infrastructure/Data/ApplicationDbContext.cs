using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PageManagementSystem.Core.Entities;

namespace PageManagementSystem.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageContent> PageContents { get; set; }
        public DbSet<PageData> PageData { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Page - PageData (Bir Page birden fazla PageData'ya sahip olabilir)
            modelBuilder.Entity<PageData>()
                .HasOne(pd => pd.Page)
                .WithMany(p => p.PageData)
                .HasForeignKey(pd => pd.PageId)
                .OnDelete(DeleteBehavior.Cascade); // Sayfa silindiğinde ilişkili PageData'ları sil

            // PageData - PageContent (Bir PageData birden fazla PageContent'e sahip olabilir)
            modelBuilder.Entity<PageContent>()
                .HasOne(pc => pc.PageData)
                .WithMany(pd => pd.Contents)
                .HasForeignKey(pc => pc.PageDataId)
                .OnDelete(DeleteBehavior.Cascade); // PageData silindiğinde ilişkili PageContent'leri sil

            // Page - PageContent (Bir Page birden fazla PageContent'e sahip olabilir)
            modelBuilder.Entity<PageContent>()
                .HasOne(pc => pc.Page)
                .WithMany()
                .HasForeignKey(pc => pc.PageId)
                .OnDelete(DeleteBehavior.Restrict); // Sayfa silinirse, PageContent ilişkisini kısıtla
        }
    }
}
