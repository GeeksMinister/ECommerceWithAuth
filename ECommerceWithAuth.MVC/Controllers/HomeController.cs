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
    private readonly SignInManager<IdentityUser> _signInManager;

    public HomeController(ILogger<HomeController> logger,
                          IEmployeeClientData employeeData,
                          UserManager<IdentityUser> userManager,
                          SignInManager<IdentityUser> signInManager)
    {
        _logger = logger;
        _employeeData = employeeData;
        _userManager = userManager;
        _signInManager = signInManager;
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

    //Request.Cookies["token"]

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
