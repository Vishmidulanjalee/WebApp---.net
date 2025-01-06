using Microsoft.AspNetCore.Mvc;

namespace MyWebApp.Controllers
{
    public class VideoController : Controller 
    {
        // GET: Video/Upload
        public IActionResult Upload()
        {
            return View();
        }

        // POST: Video/Upload
        [HttpPost]
        public IActionResult Upload(IFormFile videoFile)
        {
            if (videoFile != null && videoFile.Length > 0)
            {
                // Logic to save the video file (e.g., in the "wwwroot/videos" folder)
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos", videoFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    videoFile.CopyTo(stream);
                }

                // Optionally, add success message or redirect
                TempData["SuccessMessage"] = "Video uploaded successfully!";
                return RedirectToAction("Display", new { fileName = videoFile.FileName });
            }
            return View();
        }

        // GET: Video/Display
        public IActionResult Display(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return NotFound();
            }

            var videoPath = "/videos/" + fileName;
            return View((object)videoPath);  // Pass video path to view
        }
    }
}
