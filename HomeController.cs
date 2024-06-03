 using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using myproject.Models;

namespace myproject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        // Reading the cookie from the request
        string? data = string.Empty;
        if (!HttpContext.Request.Cookies.ContainsKey("first_visit"))
        {
            // Writing the cookie to the response
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);
            HttpContext.Response.Cookies.Append("first_visit", DateTime.Now.ToString(),options);
            data = "You're visiting for the first time";
        }
        else
        {
            // Reading the cookie from the request
            data = HttpContext.Request.Cookies["first_visit"];
            data = "Welcome back, you first visited at : " + data;

        }

        return View((object)data);
    }


    public IActionResult Privacy()
    {
        Response.Cookies.Delete("first_visit"); 
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

