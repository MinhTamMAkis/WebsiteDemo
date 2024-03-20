using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteDemo.Repository;

namespace WebsiteDemo.Controllers
{
    public class MusicController : Controller
    {
        
        private readonly Datacontext _datacontext;
        public MusicController(Datacontext datacontext)
        {
           
            _datacontext = datacontext;
        }

        public IActionResult Index()
        {
            var musics = _datacontext.Musics.Include("Idol").ToList();
            var musicsCount = musics.Count;
            ViewBag.MusicsCount = musicsCount;

            var Idols = _datacontext.Idols.ToList();
            var IdolsCount = Idols.Count;
            ViewBag.IdolsCount = IdolsCount;


            var getName = _datacontext.Idols.ToList();
            var musicNames = _datacontext.Musics.Select(m => m.Name).ToList();
            ViewBag.MusicNames = musicNames;
            return View(musics);
        }

        [HttpGet]
        public IActionResult GetMusics()
        {
            var musics = _datacontext.Musics
                .Include(m => m.Idol) // Assuming there's a navigation property named Ido
                .ToList();

            return Json(musics);
        }
    }
}
