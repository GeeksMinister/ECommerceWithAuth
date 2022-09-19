using static DataAccessLibrary.Data.Repositories.OrderRepository;

namespace DataAccessLibrary.Data.Repositories.Contracts;

public interface IOrderRepository
{
    Task<List<Order>> GetAllOrders();
    Task<List<SalesAndWeatherRelation>> GetSalesSummary();
}