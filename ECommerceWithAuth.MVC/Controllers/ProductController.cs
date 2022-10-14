namespace ECommerceWithAuth.MVC.Controllers;

using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Administration")]
public class ProductController : Controller
{
    private readonly IProductClientData _productClientData;
    private readonly IMapper _mapper;

    public ProductController(IProductClientData productData, IMapper mapper)
    {
        _productClientData = productData;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(Currency currency)
    {
        try
        {
            var token = Request.Cookies["token"]!;
            var products = await _productClientData.GetAllProducts("Bearer " + token);

            decimal exchangeRate = 0;
            exchangeRate = await SetUpPrice(currency);
            if (exchangeRate > 0) products.ForEach(prod => prod.Price /= exchangeRate);
            if (exchangeRate == 0) currency = Currency.SEK;

            return View((products, currency));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    private async Task<decimal> SetUpPrice(Currency currency)
    {
        try
        {
            switch (currency)
            {
                case Currency.SEK: return 0;
                case Currency.USD: return await _productClientData.RequestExchangeRate(Currency.USD);
                case Currency.EUR: return await _productClientData.RequestExchangeRate(Currency.EUR);
                case Currency.GBP: return await _productClientData.RequestExchangeRate(Currency.GBP);
                case Currency.CAD: return await _productClientData.RequestExchangeRate(Currency.CAD);
                case Currency.CHF: return await _productClientData.RequestExchangeRate(Currency.CHF);
                case Currency.JPY: return await _productClientData.RequestExchangeRate(Currency.JPY);
                case Currency.NOK: return await _productClientData.RequestExchangeRate(Currency.NOK);
                case Currency.DKK: return await _productClientData.RequestExchangeRate(Currency.DKK);
                default: return 0;
            }
        }
        catch (Exception)
        {
            return 0;
        }
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
            await _productClientData.AddNewProduct(productDto);
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
            var Product = await _productClientData.GetProductById(id);
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
            await _productClientData.UpdateProduct(id, ProductDto);
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
            var product = await _productClientData.GetProductById(id);
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
            await _productClientData.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    public async Task<IActionResult> PatchProductDiscountRate(Guid id, string value, string path = "discountRate")
    {
        try
        {
            var product = await _productClientData.GetProductById(id);
            var productDto = _mapper.Map<ProductDto>(product);
            productDto.DiscountRate = decimal.Parse(value);
             await _productClientData.UpdateProduct(id, productDto);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    public async Task<IActionResult> PatchProductDiscountUntil(Guid id, string value, string path = "discountUntil")
    {
        try
        {
            var product = await _productClientData.GetProductById(id);
            var productDto = _mapper.Map<ProductDto>(product);
            productDto.DiscountUntil = DateTime.Parse(value);
            await _productClientData.UpdateProduct(id, productDto);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
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
