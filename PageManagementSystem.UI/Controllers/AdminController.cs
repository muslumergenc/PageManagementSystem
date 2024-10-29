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
        private readonly IPageDataService _pageDataService;
        private readonly IPageContentService _pageContentService;
        public AdminController(IPageService pageService, IPageDataService pageDataService, IPageContentService pageContentService)
        {
            _pageService = pageService;
            _pageDataService = pageDataService;
            _pageContentService = pageContentService;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Page
        public async Task<IActionResult> PageIndex()
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
                return View();
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
        #endregion

        #region PageData

        public async Task<IActionResult> PageDataIndex(int pageId)
        {
            if (pageId <= 0)
            { 
                var pageDataList = await _pageDataService.GetAllPageDataAsync();
                return View(pageDataList);
            }
            var pageDataListAsync = await _pageDataService.GetAllPageDataAsync(pageId);
            ViewBag.PageId = pageId;
            return View(pageDataListAsync);
        }
        [HttpGet]
        public async Task<IActionResult> CreatePageData(int pageId)
        {
            var page = await _pageService.GetPageByIdAsync(pageId);
            if (page == null)
            {
                return NotFound("Geçersiz PageId: Bu sayfa mevcut değil.");
            }

            var pageData = new PageData { PageId = pageId };
            return View(pageData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePageData(PageData model)
        {
            var page = await _pageService.GetPageByIdAsync(model.PageId);
            if (page == null)
            {
                ModelState.AddModelError("", "Belirtilen PageId geçersiz. Lütfen geçerli bir sayfa seçin.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                await _pageDataService.AddPageDataAsync(model);
                return RedirectToAction("PageDataIndex", new { pageId = model.PageId });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditPageData(int id)
        {
            if (id!=0 || id!=null)
            {
                var pageData = await _pageDataService.GetPageDataByIdAsync(id);
                if (pageData == null)
                {
                    return NotFound();
                }
                return View(pageData);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPageData(PageData model)
        {
            if (ModelState.IsValid)
            {
                await _pageDataService.UpdatePageDataAsync(model);
                return RedirectToAction("PageDataIndex", new { pageId = model.PageId });
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeletePageData(int id,string? d)
        {
            var pageData = await _pageDataService.GetPageDataByIdAsync(id);
            if (pageData == null)
            {
                return NotFound();
            }
            return View(pageData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePageData(int id)
        {
            var pageData = await _pageDataService.GetPageDataByIdAsync(id);
            if (pageData != null)
            {
                await _pageDataService.DeletePageDataAsync(id);
                return RedirectToAction("PageDataIndex", new { pageId = pageData.PageId });
            }
            return View("Silme İşlemi Başarılı");
        }
        #endregion

        #region PageContent

        public async Task<IActionResult> PageContentIndex(int pageDataId)
        {
            if (pageDataId<=0)
            {
                var pageContentList = await _pageContentService.GetAllContentAsync();
                return View(pageContentList);
            }
            var pageContentListAsync = await _pageContentService.GetAllPageContentAsync(pageDataId);
            ViewBag.PageDataId = pageDataId; // İçerik bileşenlerinin ait olduğu içerik grubunu View'a iletmek için
            return View(pageContentListAsync);
        }
        [HttpGet]
        public async Task<IActionResult> CreatePageContent(int pageDataId)
        {
            var pageData = await _pageDataService.GetPageDataByIdAsync(pageDataId);
            if (pageData == null)
            {
                return NotFound("Geçersiz PageDataId: Bu içerik grubu mevcut değil.");
            }

            var pageContent = new PageContent { PageId = pageData.PageId, PageDataId = pageDataId };
            return View(pageContent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePageContent(PageContent model)
        {
            var pageData = await _pageDataService.GetPageDataByIdAsync(model.PageDataId);
            if (pageData == null)
            {
                ModelState.AddModelError("", "Belirtilen PageDataId geçersiz. Lütfen geçerli bir içerik grubu seçin.");
                return View(model);
            }

            model.PageId = pageData.PageId; // Doğru PageId ile ilişki kur

            if (ModelState.IsValid)
            {
                await _pageContentService.AddPageContentAsync(model);
                return RedirectToAction("PageContentIndex", new { pageDataId = model.PageDataId });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditPageContent(int id)
        {
            var pageContent = await _pageContentService.GetPageContentByIdAsync(id);
            if (pageContent == null)
            {
                return NotFound();
            }
            return View(pageContent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPageContent(PageContent model)
        {
            if (ModelState.IsValid)
            {
                await _pageContentService.UpdatePageContentAsync(model);
                return RedirectToAction("PageContentIndex", new { pageDataId = model.PageId });
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeletePageContent(int id)
        {
            var pageContent = await _pageContentService.GetPageContentByIdAsync(id);
            if (pageContent == null)
            {
                return NotFound();
            }
            return View(pageContent);
        }

        [HttpPost, ActionName("DeletePageContent")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePageContentConfirmed(int id,string? s)
        {
            var pageContent = await _pageContentService.GetPageContentByIdAsync(id);
            if (pageContent != null)
            {
                await _pageContentService.DeletePageContentAsync(id);
                return RedirectToAction("PageContentIndex", new { pageDataId = pageContent.PageId });
            }
            return NotFound();
        }
        #endregion
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