using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using MimeKit;
using Portfolio.Misc.Services;
using Portfolio.Models;

namespace Portfolio.Controllers;

public class HomeController : Controller
{
    private readonly IHtmlLocalizer<HomeController> _localizer;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IHtmlLocalizer<HomeController> localizer)
    {
        _localizer = localizer;
        _logger = logger;
    }
    
    public IActionResult Index()
    {
        var localized = _localizer["Hi"];
        ViewData["Hi"] = localized;
        return View();
    }
  
    
    /*public IActionResult SendEmail()
    {
        _emailSender.SendEmail("content", "header");
        return RedirectToAction("Index");
    }*/
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}