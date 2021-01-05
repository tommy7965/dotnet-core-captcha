using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dotnet_mvc.Models;
using System.Drawing;
using System;
using System.IO;
using dotnet_mvc.Services;

namespace dotnet_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["testString"] = "hello world";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult GenerateImage()
        {
            String strRandom = new Random().Next(1, 9999).ToString();
            Console.Out.WriteLine("strRandom -> " + strRandom);
            Bitmap bmp = new GenerateBitmapService().createCaptcha(200, 80, strRandom);
            Graphics g = Graphics.FromImage(bmp);
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            g.Dispose();
            bmp.Dispose();
            Console.Out.WriteLine("test console");
            return File(ms.ToArray(), "image/jpeg");
        }
    }
}
