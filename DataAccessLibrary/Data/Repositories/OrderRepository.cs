using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace DataAccessLibrary.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ECommerceDbContext _dbContext;
    private readonly IConfiguration _config;

    public OrderRepository(ECommerceDbContext dbContext, IConfiguration config)
    {
        _dbContext = dbContext;
        _config = config;
    }

    public async Task<List<Order>> GetAllOrders()
    {
        return await _dbContext.Order.Include(order => order.OrderItems).
                        ThenInclude(order => order.Product).ToListAsync();
    }

    public async Task<object> GetSellsSummary()
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

    public async Task<object> GetTopSold()
    {
        var items = await _dbContext.OrderItems.Include(item => item.Product)
            .Include(item => item.Order).ToListAsync();

        var summary = items.Where(item =>
            (DateTime.Now.Month - DateTime.Parse(item.Order.OrderPlaced).AddMonths(-1).Month) >= 1)
            .GroupBy(item => item.ProductId)
            .Select(selected => new
            {
                ProductId = selected.Key,
                ProductName = selected.Select(item => item.Product.Name).FirstOrDefault(),
                TotalSold = selected.Sum(item => item.Quantity)
            }).OrderByDescending(selected => selected.TotalSold);
        return summary;
    }

    public async Task<List<Product>> GetProductShortages()
    {
        var products = await _dbContext.Product.Where(prod => prod.Quantity <= 10).ToListAsync();
        return products;
    }


    public async Task<List<SellsAndWeatherRelation>> GetWeatherAndSellsRelation()
    {
        var orders = await GetAllOrders();
        List<SellsAndWeatherRelation> relation = new List<SellsAndWeatherRelation>();

        orders.GroupBy(order => order.OrderPlaced)
            .Select(sellDate => new
            {
                Date = sellDate.Key,
                TotalOrders = sellDate.Count(),
            }).OrderByDescending(order => order.TotalOrders).Take(3).ToList()
            .ForEach(date => relation.Add(new SellsAndWeatherRelation(
                date.Date, date.TotalOrders, GetWeatherByDate(date.Date).Result)));

        orders.GroupBy(order => order.OrderPlaced)
            .Select(sellDate => new
            {
                Date = sellDate.Key,
                TotalOrders = sellDate.Count(),
            }).OrderBy(order => order.TotalOrders).Take(3).ToList()
            .ForEach(date => relation.Add(new SellsAndWeatherRelation(
            date.Date, date.TotalOrders, GetWeatherByDate(date.Date).Result)));

        return relation;
    }

    private async Task<Weather> GetWeatherByDate(string date)
    {
        try
        {
            string apiLocation = _config["WeatherApi:Location"];
            string key = _config["WeatherApi:Key"];
            string specification = "&include=current&elements=temp,humidity,description";
            var requestLink = $"{apiLocation}/{date}?{key}{specification}";
            using HttpClient client = new HttpClient();

            var response = await client.GetAsync(requestLink);
            var content = await response.Content.ReadAsStringAsync();
            var weather = JsonConvert.DeserializeObject<ApiResponse>(content);

            return weather!.Days.FirstOrDefault()!;
        }
        catch (Exception ex)
        {
            return new Weather(ex.Message, "Error", "Error");
        }

    }

    public record SellsAndWeatherRelation(string Date, int TotalOrders, Weather Weather);
    public record Weather(string Temp, string Humidity, string Description);
    public record ApiResponse(Weather[] Days);

    public async Task<object> GetDistance(string destination)
    {
        try
        {
            string apiLocation = _config["DistanceMatrixApi:Location"];
            string key = _config["DistanceMatrixApi:Key"];
            apiLocation = apiLocation.Replace("[DSTN]", destination);

            var requestLink = $"{apiLocation}{key}";
            using HttpClient client = new HttpClient();

            var respnse = await client.GetFromJsonAsync<DistanceMatrix>(requestLink);

            var distance = respnse!.rows[0].elements[0].distance.text;
            var duration = respnse.rows[0].elements[0].duration.text;
            return new { Distance = distance, Duration = duration };
        }
        catch (Exception ex)
        {
            return new { Error = ex.Message };
        }

    }

}
