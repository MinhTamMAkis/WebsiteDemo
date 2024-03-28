using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Index()
        {
            
            return View();
        }

    }
}
