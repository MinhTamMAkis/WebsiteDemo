using System.ComponentModel.DataAnnotations;

namespace WebsiteDemo.Models
{
    public class MusicModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}
