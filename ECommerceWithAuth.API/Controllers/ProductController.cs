using Microsoft.AspNetCore.JsonPatch;

namespace ECommerceWithAuth.API.Controllers;

[Route("api/")]
[ApiController]
public class ProductController : ControllerBase
{
	private readonly IProductRepository _productRepository;
	private readonly IMapper _mapper;

	public ProductController(IProductRepository productRepository, IMapper mapper)
	{
		_productRepository = productRepository;
		_mapper = mapper;
	}

    [HttpGet("Product")]
	//[Authorize(Roles = "Admin")]
	public async Task<IActionResult> GetAllProducts()
	{
		var result = await _productRepository.GetAllProducts();
        if (result is null) return NoContent();
        Response.Headers.Append("Total_Products", result.Count.ToString());
		return Ok(result);
	}

	[HttpGet("Product/{guid}")]
	public async Task<IActionResult> GetProductById(Guid guid)
	{
		var result = await _productRepository.GetProductById(guid);
		if (result is null) return NotFound();
		return Ok(result);
	}

	[HttpGet("Categories")]
	public async Task<IActionResult> GetCategories()
	{
		var result = await _productRepository.GetCategories();
        if (result is null) return NotFound();
        return Ok(result);
    }

	[HttpPost("Product")]
	public async Task<IActionResult> AddNewProduct(ProductDto productDto)
	{
		var Product = _mapper.Map<Product>(productDto);
        var result = await _productRepository.AddNewProduct(Product);
		return Ok(result);
	}

	[HttpPut("Product/{guid}")]
	public async Task<IActionResult> UpdateProduct(Guid guid, ProductDto productDto)
	{
		var product = await _productRepository.GetProductById(guid);
		if (product is null) return NotFound();
		product = _mapper.Map<Product>(productDto);
		product.Guid = guid;
		product = await _productRepository.UpdateProduct(product);
		if (product is null) return NotFound();
		return Ok(product);
	}


	[HttpPatch("Product/{guid}")]
	public async Task<IActionResult> PatchProductPatch(Guid guid, JsonPatchDocument<Product> patch)
	{
		var product = await _productRepository.GetProductById(guid);
		if (product is null) return NotFound();
		product = await _productRepository.UpdateProductPatch(product.Guid, patch);
		if (product is null) return NotFound();

		return Ok(product);
	}

    [HttpDelete("Product/{guid}")]
	public async Task<IActionResult> DeleteProduct(Guid guid)
	{
		var result = await _productRepository.GetProductById(guid);
		if (result is null) return NotFound();
		await _productRepository.DeleteProduct(guid);
		return Ok($"Product with Id: {guid} Was Successfully Removed.");
	}




}
