namespace DataAccessLibrary.Data.Repositories.Contracts;
public interface IProductRepository
{
    Task<List<Product>> GetAllProducts();
}