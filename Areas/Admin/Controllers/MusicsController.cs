using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebsiteDemo.Models;
using WebsiteDemo.Repository;

namespace WebsiteDemo.Areas.Admin.Controllers

{
    [Area("Admin")]
    public class MusicsController : Controller
    {
        private readonly Datacontext _datacontext;
        private readonly IWebHostEnvironment _environment;
        public MusicsController(Datacontext datacontext, IWebHostEnvironment environment)
        {

            _datacontext = datacontext;
            _environment = environment;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(MusicModel music)
        {
            ViewBag.Idols = new SelectList(_datacontext.Idols, "Id", "Name", music.IdolId);
            if (ModelState.IsValid)
            {

                music.Slug = music.Name.Replace(" ","-");
                var slug = await _datacontext.Musics.FirstOrDefaultAsync(p => p.Slug == music.Slug);
                if(slug != null)
                {
                    ModelState.AddModelError("", "Error music has in database");
                    return View(music);
                }
                
                    if(music.ImageUpload != null)
                    {
                        string uploadsDir = Path.Combine(_environment.WebRootPath,"Imgae_Musics");
                        /*string imgageName = Guid.NewGuid().ToString() + "-" + music.ImageUpload.FileName;*/
                        string imgageName = music.Slug + ".webp";
                        string filePath = Path.Combine(uploadsDir, imgageName);

                        FileStream fs = new FileStream(filePath, FileMode.Create);
                        await music.ImageUpload.CopyToAsync(fs);
                        fs.Close();
                        music.Image = imgageName;
                    }
                    if (music.Mp3Upload != null)
                    {
                        string uploadsDir = Path.Combine(_environment.WebRootPath, "music");
                        string filemusicName = music.Name+".mp3";
                        string filePath = Path.Combine(uploadsDir, filemusicName);

                        FileStream fs = new FileStream(filePath, FileMode.Create);
                        await music.Mp3Upload.CopyToAsync(fs);
                        fs.Close();
                        music.File = filemusicName;
                    }

                _datacontext.Add(music);
                await _datacontext.SaveChangesAsync();
                TempData["success"] = "Success";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = " Model Error";
                List<string> errors = new List<string>();
                foreach(var value in ModelState.Values)
                {
                    foreach(var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }
            return View(music);
        }

        public async Task<IActionResult> Edit (int Id)
        {
            MusicModel music = await _datacontext.Musics.FindAsync(Id);
            ViewBag.Idols = new SelectList(_datacontext.Idols, "Id", "Name");

            return View(music);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int Id,MusicModel music)
        {
            ViewBag.Idols = new SelectList(_datacontext.Idols, "Id", "Name", music.IdolId);
            if (ModelState.IsValid)
            {

                music.Slug = music.Name.Replace(" ", "-");
                var slug = await _datacontext.Musics.FirstOrDefaultAsync(p => p.Slug == music.Slug);
                MusicModel musicId = await _datacontext.Musics.FindAsync(Id);

                if (slug != null)
                {
                    ModelState.AddModelError("", "Error music has in database");
                    return View(music);
                }
                
                
               
                if (music.ImageUpload != null)
                {
                    
                    string uploadsDir = Path.Combine(_environment.WebRootPath, "Imgae_Musics");
                    string imgageName = Guid.NewGuid().ToString() + "-" + music.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadsDir, imgageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await music.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                    music.Image = imgageName;
                }

                if (music.Mp3Upload != null)
                {
                    string uploadsDir = Path.Combine(_environment.WebRootPath, "music");
                    string filemusicName = music.Name + ".mp3";
                    string filePath = Path.Combine(uploadsDir, filemusicName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await music.Mp3Upload.CopyToAsync(fs);
                    fs.Close();
                    music.File = filemusicName;
                }

                _datacontext.Update(music);
                await _datacontext.SaveChangesAsync();
                TempData["success"] = "Success";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = " Model Error";
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }
            return View(music);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            // Find the music by ID
            MusicModel music = await _datacontext.Musics.FindAsync(Id);

            if (music == null)
            {
                return NotFound(); // Return 404 if music not found
            }

            // Delete associated image file
            if (!string.IsNullOrEmpty(music.Image))
            {
                string imagePath = Path.Combine(_environment.WebRootPath, "Imgae_Musics", music.Image);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            // Delete associated MP3 file
            if (!string.IsNullOrEmpty(music.File))
            {
                string musicPath = Path.Combine(_environment.WebRootPath, "music", music.File);
                if (System.IO.File.Exists(musicPath))
                {
                    System.IO.File.Delete(musicPath);
                }
            }

            // Remove the music from the database context
            _datacontext.Musics.Remove(music);
            await _datacontext.SaveChangesAsync(); // Save changes to the database

            TempData["success"] = "Music deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
