using BlogApp.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace BlogApp.Data
{
    public class Tag
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez.")]
        [MaxLength(50)]
        public string TagName { get; set; }

        public List<Blog>? TagBlogs { get; set; } = new List<Blog>();
    }
}
