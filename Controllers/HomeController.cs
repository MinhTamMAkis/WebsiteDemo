using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebsiteDemo.Models;
using WebsiteDemo.Repository;

namespace WebsiteDemo.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly Datacontext _datacontext;
		public HomeController(ILogger<HomeController> logger, Datacontext datacontext)
		{
			_logger = logger;
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

            return View(musics);
		}

		public IActionResult Idol()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
