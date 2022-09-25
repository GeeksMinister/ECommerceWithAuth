namespace ECommerceWithAuth.MVC.Controllers;

using Microsoft.AspNetCore.Authorization;

//[Authorize(Roles = "Administration")]
public class ProductController : Controller
{
    private readonly IProductClientData _productData;
    private readonly IMapper _mapper;

    public ProductController(IProductClientData productData, IMapper mapper)
    {
        _productData = productData;
        _mapper = mapper;
    }
    
    public async Task<IActionResult> Index()
    {
        var token = Request.Cookies["token"]!;
        var result = await _productData.GetAllProducts("Bearer " + token);
        return View(result);
    }

    public IActionResult AddProduct()
    {
        try
        {            
            return View();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductDto productDto)
    {
        try
        {
            if (ModelState.IsValid == false) return View(productDto);
            await _productData.AddNewProduct(productDto);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

















}
