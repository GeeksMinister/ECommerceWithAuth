namespace DataAccessLibrary.Data.Repositories.Contracts;

public interface IProductRepository
{
    Task<Product> AddNewProduct(Product product);
    Task DeleteProduct(Guid productId);
    Task<List<Product>> GetAllProducts();
    Task<Product?> GetProductById(Guid productId);
    Task<Product> UpdateProduct(Product updated);
}