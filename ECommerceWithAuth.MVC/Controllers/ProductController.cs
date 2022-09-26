namespace ECommerceWithAuth.MVC.Controllers;

using Microsoft.AspNetCore.Authorization;
using NuGet.Protocol.Core.Types;

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

    public async Task<IActionResult> UpdateProduct(Guid id)
    {
        try
        {
            var Product = await _productData.GetProductById(id);
            if (Product == null) return NotFound();
            return View(Product);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProduct_Post(ProductDto ProductDto, Guid id)
    {
        try
        {
            if (ModelState.IsValid == false) return View(ProductDto);
            await _productData.UpdateProduct(id, ProductDto);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        try
        {
            var product = await _productData.GetProductById(id);
            if (product == null) return NotFound();
            return View(product);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteProduct_Post(Guid id)
    {
        try
        {
            await _productData.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    public async Task<IActionResult> PatchProductDiscountRate(Guid id, string value, string path = "discountRate")
    {
        var product = await _productData.GetProductById(id);
        var productDto = _mapper.Map<ProductDto>(product);
        productDto.DiscountRate = decimal.Parse(value);
         await _productData.UpdateProduct(id, productDto);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> PatchProductDiscountUntil(Guid id, string value, string path = "discountUntil")
    {
        var product = await _productData.GetProductById(id);
        var productDto = _mapper.Map<ProductDto>(product);
        productDto.DiscountUntil = DateTime.Parse(value);
        await _productData.UpdateProduct(id, productDto);

        return RedirectToAction(nameof(Index));
    }


    // Failed with the making a successful HttpPatch request  :(
      // Nor RefitClient or HttpClient worked.
    //public IActionResult PatchProductDiscountRate(Guid id, string value, string path = "discountRate")
    //{

    //    PatchProduct_Post(id, value, path);
    //    return RedirectToAction(nameof(Index));
    //}


    //public IActionResult PatchProductDiscountUntil(Guid id, string value, string path = "discountUntil")
    //{

    //    PatchProduct_Post(id, path, value);
    //    return RedirectToAction(nameof(Index));
    //}

    //public async void PatchProduct_Post(Guid guid, string value, string path)
    //{
    //    var patchObject =  new  { Path = path, Value = value, Op = "replace"}; 
    //    await _productData.PatchProduct(guid, patchObject);        
    //}











}
