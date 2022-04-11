using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Portfolio.Misc.Services;
using Portfolio.Models;

namespace Portfolio.Controllers;

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