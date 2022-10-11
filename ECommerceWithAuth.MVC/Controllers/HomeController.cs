using ECommerceWithAuth.MVC.Models;
using System.Diagnostics;
using System.Net;

namespace ECommerceWithAuth.MVC.Controllers;

public class HomeController : Controller
{
    private readonly IEmployeeClientData _employeeData;
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(IEmployeeClientData employeeData,
                          UserManager<IdentityUser> userManager)
    {
        _employeeData = employeeData;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        try
        {
            if (string.IsNullOrEmpty(Request.Cookies["token"]) && (User?.Identity?.IsAuthenticated ?? false))
            {
                var userId = _userManager.GetUserId(User);
                var loginInfo = _userManager.GetUserName(User);
                var token = string.Empty;
                if (User?.IsInRole("Administration") ?? false)
                {
                    token = _employeeData.RequestToken(loginInfo, userId).Result;
                }
                else if (User?.IsInRole("Customer") ?? false)
                {
                    token = _employeeData.RequestCustomerToken(loginInfo, userId).Result;
                }
                Response.Cookies.Append("token", token);
            }
            else if ((User?.Identity?.IsAuthenticated ?? true) == false)
            {
                Response.Cookies.Delete("token");                
            }
            return View();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
