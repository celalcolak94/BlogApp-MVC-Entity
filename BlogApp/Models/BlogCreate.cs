using BlogApp.Data;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class BlogCreate
    {
        [Required(ErrorMessage = "Başlık alanını boş bırakamazsınız.")]
        [MaxLength(20)]
        public string Tittle { get; set; }

        [Required(ErrorMessage = "Açıklama alanını boş bırakamazsınız.")]
        [MaxLength(20)]
        public string Content { get; set; }

        [Required(ErrorMessage = "Bu alanı boş bırakamazsınız.")]
        public string Publish { get; set; }
        public int? BlogCategoryId { get; set; }
        public List<int> BlogTagsId { get; set; }

    }
}
