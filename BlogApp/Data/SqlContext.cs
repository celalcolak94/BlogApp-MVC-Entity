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


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=BlogDb;Trusted_Connection=True;MultipleActiveResultSets=true;");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
