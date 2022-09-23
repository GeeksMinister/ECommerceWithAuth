
namespace ECommerceWithAuth.MVC.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

public class ProductController : Controller
{
    private readonly IProductClientData _productData;
    private readonly IEmployeeClientData _employeeData;
    private readonly UserManager<IdentityUser> _userManager;

    public ProductController(IProductClientData productData, ECommerceDbContext dbContext, IEmployeeClientData employeeData, UserManager<IdentityUser> userManager)
    {
        _productData = productData;
        _employeeData = employeeData;
        _userManager = userManager;
    }

    [Authorize(Roles = "Administration")]
    public async Task<IActionResult> Index()
    {
        var token = Request.Cookies["token"]!;
        var result = await _productData.GetAllProducts("Bearer " + token);
        return View(result);
    }
}
