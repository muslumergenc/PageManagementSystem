using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PageManagementSystem.Application.Interfaces;
using PageManagementSystem.Core.Entities;
using System.Text.RegularExpressions;
namespace PageManagementSystem.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IPageService _pageService;
        public AdminController(IPageService pageService)
        {
            _pageService = pageService;
        }
        public async Task<IActionResult> Index()
        {
            var pages = await _pageService.GetAllPagesAsync();
            return View(pages);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Page model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedAt=DateTime.Now;
                model.UpdatedAt=DateTime.Now;
                model.Slug = GenerateSlug(model.Title);
                try
                {
                    await _pageService.AddPageAsync(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Sayfa eklenirken bir hata oluştu: {ex.Message}");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Model doğrulaması başarısız oldu.");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var page = await _pageService.GetPageByIdAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Page model)
        {
            if (ModelState.IsValid)
            {
                model.UpdatedAt = DateTime.Now;
                try
                {
                    await _pageService.UpdatePageAsync(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Sayfa güncellenirken bir hata oluştu: {ex.Message}");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Model doğrulaması başarısız oldu.");
            }
            return View(model);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var page = await _pageService.GetPageByIdAsync(id);
                if (page == null)
                {
                    return Json(new { success = false, message = "Sayfa bulunamadı." });
                }

                await _pageService.DeletePageAsync(id);
                return Json(new { success = true }); // Başarı durumunda success true döner
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Sayfa silinirken bir hata oluştu: {ex.Message}" });
            }
        }


        private string GenerateSlug(string title)
        {
            title = title.ToLowerInvariant();
            title = Regex.Replace(title, @"[^a-z0-9\s-]", ""); 
            title = Regex.Replace(title, @"\s+", " ").Trim();
            title = title.Replace(" ", "-");
            return title;
        }
    }
}