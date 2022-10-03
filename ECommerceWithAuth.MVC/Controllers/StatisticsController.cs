namespace ECommerceWithAuth.MVC.Controllers;

using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Administration")]

public class StatisticsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult HiddenContent()
    {
        return View("SimonGame");
    }
}
