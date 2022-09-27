using Newtonsoft.Json.Linq;

namespace DataAccessLibrary.Services.RefitClient;

public interface IProductClientData
{
    [Get("/Product")]
    Task<List<Product>> GetAllProducts([Header("Authorization")] string authorization);

    [Get("/Product/{guid}")]
    Task<Product> GetProductById(Guid guid);

    [Post("/Product")]
    Task AddNewProduct(ProductDto productDto);

    [Get("/Categories")]
    Task<string> GetCategories();

    [Put("/Product/{guid}")]
    Task<Product> UpdateProduct(Guid guid, ProductDto productDto);

    [Delete("/Product/{guid}")]
    Task DeleteProduct(Guid guid);

    [Patch("/Product/{guid}")]
    Task PatchProduct(Guid guid, object PatchObj);
    
    [Get("/Order/RequestExchangeRate?currency={currency}")]
    Task<decimal> RequestExchangeRate(Currency currency);
}

record PatchObj(string Path, string Value, string Op = "replace");

