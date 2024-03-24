namespace WebsiteDemo.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string MusicID { get; set; }
        public string Image { get; set; }
        public MusicModel Musics { get; set; }
    }
}
