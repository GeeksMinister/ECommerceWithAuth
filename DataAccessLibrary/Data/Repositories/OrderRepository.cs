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
        var summary = orders.GroupBy(order => order.OrderPlaced)
            .Select(sellDate => new
            {
                Date = sellDate.Key,
                SalesSum = sellDate.Sum(order => order.TotalToPay),
                TotalOrders = sellDate.Count(),
                TotalItems = sellDate.Sum(order => order.OrderItems.Sum(item => item.Quantity))
            }).OrderBy(order => order.Date).ToList();

        return summary;
    }

    //public async Task<object> GetSalesSummary()
    //{
    //    var items = await _dbContext.OrderItems.Include(item => item.Product)
    //        .Include(item => item.Order).ToListAsync();

    //    var summary = items.Where(item => 
    //        (DateTime.Now.Month - DateTime.Parse(item.Order.OrderPlaced).AddMonths(-1).Month) >= 1 )
    //        .GroupBy(item => item.ProductId)
    //        .Select(i => new
    //        {
    //            ProductId = i.Key,
    //            ProductName = i.Select(tt => tt.Product.Name).FirstOrDefault(),
    //            TotalSold = i.Sum(t => t.Quantity)
    //        }).OrderByDescending(t => t.TotalSold);
    //    return summary;
    //}

    //public async Task<object> GetSalesSummary()
    //{
    //    var products = await _dbContext.Product.ToListAsync();
    //    return products.Where(prod => prod.Quantity <= 10);
    //}








}
