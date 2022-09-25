namespace DataAccessLibrary.Services.RefitClient;

public interface IProductClientData
{
    [Get("/Product")]
    Task<List<Product>> GetAllProducts([Header("Authorization")] string authorization);

    [Get("/Product/{guid}")]
    Task<Product> GetProductById(string guid);  // Might need to change back to Guid

    [Post("/Product")]
    Task<Product> AddNewProduct(ProductDto productDto);

    [Get("/Categories")]
    Task<string> GetCategories();
}
