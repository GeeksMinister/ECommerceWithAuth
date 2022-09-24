using ECommerceWithAuth.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.Diagnostics;

namespace ECommerceWithAuth.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IEmployeeClientData _employeeData;
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(ILogger<HomeController> logger,
                          IEmployeeClientData employeeData,
                          UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _employeeData = employeeData;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        if (string.IsNullOrEmpty(Request.Cookies["token"]) && (User?.Identity?.IsAuthenticated ?? false))
        {
            var employeeId = _userManager.GetUserId(User);
            var loginInfo = _userManager.GetUserName(User);
            var token = _employeeData.RequestToken(loginInfo, employeeId).Result;
            Response.Cookies.Append("token", token);
        }
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
