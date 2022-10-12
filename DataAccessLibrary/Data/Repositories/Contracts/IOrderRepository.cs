namespace DataAccessLibrary.Data.Repositories.Contracts;

public interface IOrderRepository
{
    Task<List<Order>> GetAllOrders();
    Task<object> GetDistance(string destination);
    Task<List<Product>> GetProductShortages();
    Task<object> GetSalesSummary();
    Task<object> GetTopSold();
    Task<List<SalesAndWeatherRelation>> GetWeatherAndSalesRelation();
    Task<Dictionary<string, object>> GetExchangeRates(Currency code);
    Task<decimal> RequestLiveExchangeRate(Currency code);
    Task<Order?> GetOrderById(Guid guid);
    Task<Order> AddNewOrder(Order order);
}