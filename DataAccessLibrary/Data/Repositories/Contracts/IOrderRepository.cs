using static DataAccessLibrary.Data.Repositories.OrderRepository;

namespace DataAccessLibrary.Data.Repositories.Contracts;

public interface IOrderRepository
{
    Task<List<Order>> GetAllOrders();
    Task<object> GetDistance(string destination);
    Task<Dictionary<string, decimal>> GetExchangeRates(Currency code);
    Task<List<Product>> GetProductShortages();
    Task<object> GetSellsSummary();
    Task<object> GetTopSold();
    Task<List<SellsAndWeatherRelation>> GetWeatherAndSellsRelation();
}