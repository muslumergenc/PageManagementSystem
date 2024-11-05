using App.Models;
using Microsoft.AspNetCore.Mvc;
using PageManagementSystem.Application.Interfaces;
using PageManagementSystem.Core.Entities;
using System.Diagnostics;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPageService _pageService;
      
        public HomeController(ILogger<HomeController> logger, IPageService pageService)
        {
            _pageService = pageService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var pages = await _pageService.GetAllAsync();
            return View(pages);
        }

        // GET: Page/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var page = await _pageService.GetByIdAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        // GET: Page/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Page/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Page page)
        {
            if (ModelState.IsValid)
            {
                await _pageService.CreateAsync(page);
                return RedirectToAction(nameof(Index));
            }
            return View(page);
        }

        // GET: Page/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var page = await _pageService.GetByIdAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        // POST: Page/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Page page)
        {
            if (id != page.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _pageService.UpdateAsync(page);
                return RedirectToAction(nameof(Index));
            }
            return View(page);
        }

        // GET: Page/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var page = await _pageService.GetByIdAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        // POST: Page/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _pageService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
