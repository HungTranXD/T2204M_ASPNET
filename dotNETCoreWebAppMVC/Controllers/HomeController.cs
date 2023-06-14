using dotNETCoreWebAppMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace dotNETCoreWebAppMVC.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        //[Route("/about-us")]
        public IActionResult AboutUs(string name, int numTimes = 1)
        {
            ViewData["massage"] = "Hello" + name;
            ViewData["numTimes"] = numTimes;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}