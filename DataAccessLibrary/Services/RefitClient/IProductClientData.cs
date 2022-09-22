namespace DataAccessLibrary.Services.RefitClient;

public interface IProductClientData
{
    [Get("/Product")]
    Task<List<Product>> GetAllProducts();







}
