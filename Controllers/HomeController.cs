using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DemoApp.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace DemoApp.Controllers
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
            return View();
        }

        [HttpPost]
        public IActionResult FileUpload(IFormFile file)
        {
            var lines = new List<string>();
            using (StreamReader streamReader = new StreamReader(file.OpenReadStream()))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                    lines.Add(line);
            }
            var stats = new Stats(lines);

            return View("Stats", stats);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
