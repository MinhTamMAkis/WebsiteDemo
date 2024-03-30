using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebsiteDemo.Models;
using WebsiteDemo.Repository;

namespace WebsiteDemo.Areas.Admin.Controllers

{
    [Area("Admin")]
    public class MusicsController : Controller
    {
        private readonly Datacontext _datacontext;
        public MusicsController(Datacontext datacontext)
        {

            _datacontext = datacontext;
        }
        public async Task<IActionResult> Index()
        {
            var musics = _datacontext.Musics.Include("Idol").ToList();
            return View(await _datacontext.Musics.OrderByDescending(p => p.Id).Include(p => p.Idol).ToListAsync());
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Idols = new SelectList(_datacontext.Idols, "Id", "Name");
            return View();
        }
        public async Task<IActionResult> Add(MusicModel music)
        {
            ViewBag.Idols = new SelectList(_datacontext.Idols, "Id", "Name", music.IdolId);
            return View(music);

        }

    }
}
