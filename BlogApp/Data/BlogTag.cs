using BlogApp.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class BlogTag
    {
        public int? BlogId { get; set; }

        public int? TagId { get; set; }

        public Blog? Blog { get; set; }

        public Tag? Tag { get; set; }
    }
}
