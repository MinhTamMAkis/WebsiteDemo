using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteDemo.Repository;

namespace WebsiteDemo.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class IdolsController : Controller
    {
        private readonly Datacontext _datacontext;
        private readonly IWebHostEnvironment _environment;
        public IdolsController(Datacontext datacontext, IWebHostEnvironment environment)
        {

            _datacontext = datacontext;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            var Idol = _datacontext.Idols.ToList();
            return View(Idol);
        }
    }
}   
