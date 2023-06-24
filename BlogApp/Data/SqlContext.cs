using BlogApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BlogApp.Data
{
    public class SqlContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogTag>()
                     .HasKey(pt => new { pt.BlogId, pt.TagId });
            modelBuilder.Entity<BlogTag>().HasOne(pt => pt.Blog)
                .WithMany(p => p.BlogTags)
                .HasForeignKey(pt => pt.BlogId);
            modelBuilder.Entity<BlogTag>().HasOne(pt => pt.Tag)
                .WithMany(p => p.BlogTags)
                .HasForeignKey(pt => pt.TagId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=BlogDb;Trusted_Connection=True;MultipleActiveResultSets=true;");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
