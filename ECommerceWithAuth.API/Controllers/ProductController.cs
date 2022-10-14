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
    [Authorize(Roles = "Admin, Customer")]
    public async Task<IActionResult> GetAllProducts()
	{
		try
		{
			var result = await _productRepository.GetAllProducts();
			if (result is null) return NoContent();
			Response.Headers.Append("Total_Products", result.Count.ToString());
			return Ok(result);
		}
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
	}

	[HttpGet("Product/{guid}")]
	public async Task<IActionResult> GetProductById(Guid guid)
	{
		try
		{
			var result = await _productRepository.GetProductById(guid);
			if (result is null) return NotFound();
			return Ok(result);
		}
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
	}

	[HttpGet("Categories")]
	public async Task<IActionResult> GetCategories()
	{
		try
		{
			var result = await _productRepository.GetCategories();
			if (result is null) return NotFound();
			return Ok(result);
		}
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

	[HttpPost("Product")]
	public async Task<IActionResult> AddNewProduct(ProductDto productDto)
	{
		try
		{
			var Product = _mapper.Map<Product>(productDto);
			var result = await _productRepository.AddNewProduct(Product);
			return Ok(result);
		}
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
	}

	[HttpPut("Product/{guid}")]
	public async Task<IActionResult> UpdateProduct(Guid guid, ProductDto productDto)
	{
		try
		{
			var product = await _productRepository.GetProductById(guid);
			if (product is null) return NotFound();
			product = _mapper.Map<Product>(productDto);
			product.Guid = guid;
			product = await _productRepository.UpdateProduct(product);
			if (product is null) return NotFound();
			return Ok(product);
		}
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
	}


	[HttpPatch("Product/{guid}")]
	public async Task<IActionResult> PatchProductPatch(Guid guid, [FromBody] JsonPatchDocument<Product> patch)
	{
		try
		{
			var product = await _productRepository.GetProductById(guid);
			if (product is null) return NotFound();
			product = await _productRepository.UpdateProductPatch(product.Guid, patch);
			if (product is null) return NotFound();

			return Ok(product);
		}
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
	}

    [HttpDelete("Product/{guid}")]
	public async Task<IActionResult> DeleteProduct(Guid guid)
	{
		try
		{
			await _productRepository.DeleteProduct(guid);
			var result = await _productRepository.GetProductById(guid);
			if (result is null) return NotFound();
			return Ok($"Product with Id: {guid} Was Successfully Removed.");
		}
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
	}

}
