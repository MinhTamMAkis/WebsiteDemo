using Microsoft.AspNetCore.Mvc;

namespace WebsiteDemo.Controllers
{
    public class MusicController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
