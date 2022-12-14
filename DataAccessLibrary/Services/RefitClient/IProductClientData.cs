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
    Task PatchProduct(Guid guid, PatchObj PatchObj);
    
    [Get("/Order/RequestExchangeRate/{currency}")]
    Task<decimal> RequestExchangeRate(Currency currency);    

    [Get("/Order/ExchangeRates/{currency}")]
    Task<Dictionary<string, decimal>> ExchangeRates(Currency currency);
}

