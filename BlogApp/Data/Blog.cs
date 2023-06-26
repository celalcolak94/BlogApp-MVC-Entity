using BlogApp.Models;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Data
{
    public class Blog
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık alanını boş bırakamazsınız.")]
        [MaxLength(20)]
        public string Tittle { get; set; }

        [Required(ErrorMessage = "Açıklama alanını boş bırakamazsınız.")]
        [MaxLength(20)]
        public string Content { get; set; }

        [Required(ErrorMessage = "Bu alanı boş bırakamazsınız.")]
        public bool? Publish { get; set; }

        [Required(ErrorMessage = "Bu alanı boş bırakamazsınız.")]
        public DateTime? PublishDate { get; set; }

        public int? BlogCategoryId { get; set; }
        public Category? BlogCategory { get; set; }
        public List<Tag>? BlogTags { get; set; } = new List<Tag>();


    }
}
