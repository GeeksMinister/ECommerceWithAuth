namespace ECommerceWithAuth.MVC.Controllers;

using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Customer")]
public class CartController : Controller
{
    public IActionResult Index()
    {
        try
        {
            return View();
        }
        catch (Exception)
        {
            return LocalRedirect("~/Identity/Account/Login");
        }
    }
}
