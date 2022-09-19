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

    //public async Task<object> GetSalesSummary()
    //{
    //    var orders = await GetAllOrders();
    //    var summary = orders.GroupBy(order => order.OrderPlaced)
    //        .Select(sellDate => new
    //        {
    //            Date = sellDate.Key,
    //            SalesSum = sellDate.Sum(order => order.TotalToPay),
    //            TotalOrders = sellDate.Count(),
    //            TotalItems = sellDate.Sum(order => order.OrderItems.Sum(item => item.Quantity))
    //        }).OrderBy(order => order.Date).ToList();

    //    return summary;
    //}

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


    public async Task<List<SalesAndWeatherRelation>> GetSalesSummary()
    {
        var orders = await GetAllOrders();
        var summary = orders.GroupBy(order => order.OrderPlaced)
            .Select(sellDate => new
            {
                Date = sellDate.Key,
                TotalOrders = sellDate.Count(),
            }).OrderByDescending(order => order.TotalOrders).Take(3).ToList();

        List<SalesAndWeatherRelation> relation = new List<SalesAndWeatherRelation>();

        foreach (var date in summary)
        {
            relation.Add(new SalesAndWeatherRelation(
                date.Date, date.TotalOrders, await GetWeatherByDate(date.Date)));
        }

        summary = orders.GroupBy(order => order.OrderPlaced)
            .Select(sellDate => new
            {
                Date = sellDate.Key,
                TotalOrders = sellDate.Count(),
            }).OrderBy(order => order.TotalOrders).Take(3).ToList();

        foreach (var date in summary)
        {
            relation.Add(new SalesAndWeatherRelation(
                date.Date, date.TotalOrders, await GetWeatherByDate(date.Date)));
        }

        return relation;
    }

    private async Task<Weather> GetWeatherByDate(string date)
    {
        var requestLink = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/" +
            $"Gothenburg/{date}?key=26LSVSSM6247X3EVXDJAA5HCQ&include=current&elements=temp,humidity,description";
        using HttpClient client = new HttpClient();

        var res = await client.GetAsync(requestLink);
        var content = await res.Content.ReadAsStringAsync();
        var weather = JsonConvert.DeserializeObject<ApiResponse>(content);

        return weather!.Days.FirstOrDefault()!;
    }

    public record SalesAndWeatherRelation(string Date, int TotalOrders, Weather Weather);
    public record Weather(string Temp, string Humidity, string Description);
    private record ApiResponse(Weather[] Days);






}
