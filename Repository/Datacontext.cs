using Microsoft.EntityFrameworkCore;
using WebsiteDemo.Models;

namespace WebsiteDemo.Repository
{
    public class Datacontext : DbContext
    {
           public Datacontext(DbContextOptions<Datacontext> options) : base(options) 
        {

        }
            public DbSet<MusicModel> Musics { get; set; }
    }
}
