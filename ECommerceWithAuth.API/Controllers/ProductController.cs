namespace ECommerceWithAuth.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
	private readonly IProductRepository _productRepository;

	public ProductController(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	[HttpGet]
	public async Task<IActionResult> GetAllProducts()
	{
		var result = await _productRepository.GetAllProducts();
		Response.Headers.Append("Total_Products", result.Count.ToString());
		return Ok(result);
	}
}
