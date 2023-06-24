using System.ComponentModel.DataAnnotations;

namespace BlogApp.Data
{
    public class Category
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Kategori Adını Doldurunuz")]
        [MaxLength(50)]
        public string CategoryName { get; set; }


        [Required(ErrorMessage = "Açıklama Doldurunuz")]
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
