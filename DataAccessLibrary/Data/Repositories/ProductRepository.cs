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
		return await _dbContext.Product.ToListAsync();
	}
}
