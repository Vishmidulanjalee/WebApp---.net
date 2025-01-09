/*using Microsoft.AspNetCore.Mvc;

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
*/

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MyWebApp.Controllers
{
    public class VideoController : Controller
    {
        public IActionResult AddTimestamp(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return NotFound("File name cannot be null or empty.");
            }

            // Define input and output paths
            var inputFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos", fileName);
            var outputFileName = "timestamped_" + fileName;
            var outputFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos", outputFileName);

            // Ensure FFmpeg executable path is correct
            var ffmpegPath = @"C:\Users\ADMIN\Downloads\ffmpeg-master-latest-win64-gpl-shared\ffmpeg-master-latest-win64-gpl-shared\bin"; // Update this to your FFmpeg executable path
            var arguments = $"-i \"{inputFilePath}\" -vf \"drawtext=text='%{{pts\\:hms}}':fontcolor=white:fontsize=24:x=10:y=10\" -codec:a copy \"{outputFilePath}\"";

            // Run FFmpeg process
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ffmpegPath,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();

            // Check if the timestamped video was created successfully
            if (System.IO.File.Exists(outputFilePath))
            {
                // Redirect to play the timestamped video
                var videoUrl = $"/videos/{outputFileName}";
                return Redirect(videoUrl); // Redirects to the timestamped video URL
            }

            return BadRequest("Failed to add timestamp to the video.");
        }
    }
}

