namespace DataAccessLibrary.Data.Repositories.Contracts;

public interface IProductRepository
{
    Task<Product> AddNewProduct(Product product);
    Task DeleteProduct(Guid productId);
    Task<List<Product>> GetAllProducts();
    Task<Product?> GetProductById(Guid productId);
    Task<Product> UpdateProductPatch(Guid productId, JsonPatchDocument<Product> priceProperty);
    Task<Product> UpdateProduct(Product updated);
    Task<object> GetCategories();
}