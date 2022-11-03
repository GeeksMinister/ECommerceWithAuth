namespace ECommerceWithAuth.MVC.Controllers;

using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Customer")]
public class ShopController : Controller
{
    public IActionResult Index()
    {
		try
		{
            var token = Request.Cookies["token"]!;
            return View("Index", ("bearer " + token));
        }
        catch (Exception)
        {
            return LocalRedirect("~/Identity/Account/Login");
        }
    }
}
