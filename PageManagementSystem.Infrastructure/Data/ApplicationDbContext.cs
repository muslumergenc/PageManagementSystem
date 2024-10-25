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

            modelBuilder.Entity<Page>()
                .HasMany(p => p.Contents)
                .WithOne(pc => pc.Page)
                .HasForeignKey(pc => pc.PageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Page>()
                .HasMany(p => p.PageData)
                .WithOne(pd => pd.Page)
                .HasForeignKey(pd => pd.PageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
