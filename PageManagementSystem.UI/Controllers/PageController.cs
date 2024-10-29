using Microsoft.AspNetCore.Mvc;
using PageManagementSystem.Application.Interfaces;
using PageManagementSystem.UI.Models.VMs.PageVMs;

namespace PageManagementSystem.UI.Controllers;

public class PageController : Controller
{ 
    private readonly IPageService _pageService;
    private readonly IPageDataService _pageDataService;
    private readonly IPageContentService _pageContentService;

    public PageController(IPageService pageService, IPageDataService pageDataService, IPageContentService pageContentService)
    {
        _pageService = pageService;
        _pageDataService = pageDataService;
        _pageContentService = pageContentService;
    }
    [HttpGet]
    public async Task<IActionResult> ViewPage(int pageId)
    {
        // Sayfa bilgilerini al
        var page = await _pageService.GetPageByIdAsync(pageId);
        if (page == null)
        {
            return NotFound("Sayfa bulunamadı");
        }

        // İlgili içerik gruplarını al (PageData) ve içerik bileşenlerini de dahil et
        var pageDataList = await _pageDataService.GetAllPageDataAsync(pageId);
        foreach (var pageData in pageDataList)
        {
            pageData.Contents = _pageContentService.GetAllPageContentAsync(pageData.Id).Result.ToList();
        }
        
        // Sayfa, içerik grupları ve içerik bileşenleri ile birlikte View'a gönder
        var model = new PageViewModel
        {
            Page = page,
            PageDataList = pageDataList,
            PageContent = pageDataList.SelectMany(c=>c.Contents).ToArray()
        };

        return View(model);
    }
}