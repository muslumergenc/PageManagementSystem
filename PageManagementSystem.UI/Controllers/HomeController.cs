using Microsoft.AspNetCore.Mvc;
using PageManagementSystem.Application.Interfaces;
using PageManagementSystem.UI.Models;
using System.Diagnostics;

namespace PageManagementSystem.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPageService _pageService;
        public HomeController(ILogger<HomeController> logger, IPageService pageService)
        {
            _logger = logger;
            _pageService = pageService;
        }

        public IActionResult Index()
        {
            ViewBag.Data=_pageService.GetAllPagesAsync().Result;

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
