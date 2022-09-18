namespace DataAccessLibrary.Data.Repositories;

public class OrderRepository : IOrderRepository
{
	private readonly ECommerceDbContext _dbContext;

	public OrderRepository(ECommerceDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<List<Order>> GetAllOrders()
	{
        return await _dbContext.Order.Include(order => order.OrderItems).ThenInclude(order => order.Product)
		    .ToListAsync();
	}

	public async Task<object> GetSalesSummary()
	{
		var orders = await GetAllOrders();
		var myObj = orders.GroupBy(order => order.OrderPlaced).Select(t => new
			{ Date = t.Key, SalesSum = t.Sum( order => order.TotalToPay),
            TotalOrders = t.Count(), TotalItems = t.Sum(order => order.OrderItems.Sum(item => item.Quantity))
        })
			.OrderBy(order => order.Date).ToList();


        return myObj;



    }













}
