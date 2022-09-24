
namespace ECommerceWithAuth.MVC.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class ProductController : Controller
{
    private readonly IProductClientData _productData;
    private readonly IEmployeeClientData _employeeData;
    private readonly UserManager<IdentityUser> _userManager;
    public ProductController(IProductClientData productData,
                             IEmployeeClientData employeeData,
                             UserManager<IdentityUser> userManager)
    {
        _productData = productData;
        _employeeData = employeeData;
        _userManager = userManager;
    }

    [Authorize(Roles = "Administration")]
    public async Task<IActionResult> Index()
    {
        //if (string.IsNullOrEmpty(Request.Cookies["token"]) && (User?.Identity?.IsAuthenticated ?? false))
        //{
        //    var employeeId = _userManager.GetUserId(User);
        //    var loginInfo = _userManager.GetUserName(User);
        //    var newToken = _employeeData.RequestToken(loginInfo, employeeId).Result;
        //    Response.Cookies.Append("token", newToken);
        //}

        var token = Request.Cookies["token"]!;
        var result = await _productData.GetAllProducts("Bearer " + token);
        return View(result);
    }
}
