using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebsiteDemo.Repository.Validation;

namespace WebsiteDemo.Models
{
    public class MusicModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter music name")]
        public string Name { get; set; }
        public string Slug { get; set; }

        [Required(ErrorMessage = "Please insert the file")]
        public string File { get; set; }

        [Required(ErrorMessage = "Please insert the file")]

        public string Image { get; set; }
        [Required(ErrorMessage = "Please insert the file")]

        public int IdolId { get; set; }

        public IdolModel Idol{ get; set; }
        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }
        [NotMapped]
        [FileExtension]
        public IFormFile Mp3Upload { get; set; }
    }
}
