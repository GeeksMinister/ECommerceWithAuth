namespace DataAccessLibrary.Data.Repositories.Contracts;

public interface IOrderRepository
{
    Task<List<Order>> GetAllOrders();
    Task<object> GetSalesSummary();
}