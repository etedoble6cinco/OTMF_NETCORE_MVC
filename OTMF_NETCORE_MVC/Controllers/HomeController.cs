using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OTMF_NETCORE_MVC.Models;
using System.Diagnostics;

namespace OTMF_NETCORE_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OTMFContext _context;

        public HomeController(ILogger<HomeController> logger, OTMFContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index()
        {
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
    }
}