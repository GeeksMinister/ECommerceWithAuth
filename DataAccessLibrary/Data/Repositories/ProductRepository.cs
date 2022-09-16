using DataAccessLibrary.Data.Repositories.Contracts;
using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Data.Repositories;

public class ProductRepository : IProductRepository
{
	private readonly ECommerceDbContext _dbContext;

	public ProductRepository(ECommerceDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<List<Product>> GetAllProducts()
	{
		return await _dbContext.Product.Include(prod => prod.Category).ToListAsync();
	}

	public async Task<Product?> GetProductById(Guid productId)
	{
		var result = await _dbContext.Product.Include(prod => prod.Category)
				   .FirstOrDefaultAsync(prod => prod.Guid.Equals(productId));
		return result;
	}

	public async Task<Product> AddNewProduct(Product product)
	{
		var result = await _dbContext.AddAsync(product);
		await _dbContext.SaveChangesAsync();

		return result.Entity;
	}

	public async Task<Product> UpdateProduct(Product updated)
	{
		var product = await _dbContext.Product.FirstOrDefaultAsync(prod => prod.Guid.Equals(updated.Guid));
		if (product == null) return null!;

		product.Name = updated.Name;
		product.ImageURL = updated.ImageURL;
		product.Quantity = updated.Quantity;
		product.DiscountUntil = updated.DiscountUntil;
		product.DiscountRate = updated.DiscountRate;
		product.Price = updated.Price;
		product.CategoryId = updated.CategoryId;
		await _dbContext.SaveChangesAsync();

		return product;
	}

	public async Task DeleteProduct(Guid productId)
	{
		var result = await _dbContext.Product.FirstOrDefaultAsync(prod => prod.Guid.Equals(productId));
		if (result == null) return;

		_dbContext.Product.Remove(result);
		await _dbContext.SaveChangesAsync();
	}






}
