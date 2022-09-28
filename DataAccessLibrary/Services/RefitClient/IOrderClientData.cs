namespace DataAccessLibrary.Services.RefitClient;

public interface IOrderClientData
{
    [Get("/Order")]
    Task<List<OrderDto>> GetAllOrder();

    [Get("/Order/{guid}")]
    List<Order> GetOrderById(Guid guid);

    [Get("/Order/DistanceMatrix/{destination}")]
    Task<Destination> RequestDistanceMatrix(string destination);
}
