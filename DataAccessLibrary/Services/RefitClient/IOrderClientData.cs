namespace DataAccessLibrary.Services.RefitClient;

public interface IOrderClientData
{
    [Get("/Order")]
    Task<List<OrderDto>> GetAllOrder([Header("Authorization")] string authorization);

    [Get("/Order/{guid}")]
    Task<OrderDto> GetOrderById(Guid guid);

    [Get("/Order/DistanceMatrix/{destination}")]
    Task<Destination> RequestDistanceMatrix(string destination);

    [Post("/Order")]
    Task AddNewOrder(OrderDto orderDto);
}
