using static DataAccessLibrary.Data.Repositories.OrderRepository;

namespace DataAccessLibrary.Data.Repositories.Contracts;

public interface IOrderRepository
{
    Task<List<Order>> GetAllOrders();
    Task<object> GetDistance(string destination);
    Task<List<Product>> GetProductShortages();
    Task<object> GetSellsSummary();
    Task<object> GetTopSold();
    Task<List<SellsAndWeatherRelation>> GetWeatherAndSellsRelation();
    Task<Dictionary<string, object>> GetExchangeRates(Currency code);
    Task<decimal> RequestLiveExchangeRate(Currency code);
    Task<Order?> GetOrderById(Guid guid);
}