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
        return await _dbContext.Order.OrderBy(order => order.OrderPlaced).Include(order => order.OrderItems).
                        ThenInclude(item => item.Product).ToListAsync();
    }

    public async Task<Order?> GetOrderById(Guid guid)
    {
        return await _dbContext.Order.Include(order => order.OrderItems)
            .ThenInclude(item => item.Product).FirstOrDefaultAsync(order => order.OrderId.Equals(guid));
    }

    public async Task<Order> AddNewOrder(Order order)
    {
        var result = await _dbContext.Order.AddAsync(order);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<object> GetSalesSummary()
    {
        var orders = await GetAllOrders();
        var summary = orders.GroupBy(order => order.OrderPlaced)
            .Select(salelDate => new
            {
                Date = salelDate.Key,
                SalesSum = salelDate.Sum(order => order.TotalToPay),
                TotalOrders = salelDate.Count(),
                TotalItems = salelDate.Sum(order => order.OrderItems.Sum(item => item.Quantity))
            }).OrderBy(order => order.Date);

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
                ProductName = selected.Select(item => item.Product!.Name).FirstOrDefault(),
                TotalSold = selected.Sum(item => item.Quantity)
            }).OrderByDescending(selected => selected.TotalSold).Take(5);
        return summary;
    }

    public async Task<List<Product>> GetProductShortages()
    {
        var products = await _dbContext.Product.Where(prod => prod.Quantity <= 10)
            .Include(prod => prod.Category).ToListAsync();
        return products;
    }

    public async Task<List<SalesAndWeatherRelation>> GetWeatherAndSalesRelation()
    {
        var orders = await GetAllOrders();
        List<SalesAndWeatherRelation> relation = new List<SalesAndWeatherRelation>();

        orders.GroupBy(order => order.OrderPlaced)
            .Select(saleDate => new
            {
                Date = saleDate.Key,
                TotalOrders = saleDate.Count(),
            }).OrderByDescending(order => order.TotalOrders).Take(3).ToList()
            .ForEach(date => relation.Add(new SalesAndWeatherRelation(
                date.Date, date.TotalOrders, GetWeatherByDate(date.Date).Result)));

        orders.GroupBy(order => order.OrderPlaced)
            .Select(saleDate => new
            {
                Date = saleDate.Key,
                TotalOrders = saleDate.Count(),
            }).OrderBy(order => order.TotalOrders).Take(3).ToList()
            .ForEach(date => relation.Add(new SalesAndWeatherRelation(
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
            var weather = JsonConvert.DeserializeObject<WeatherApiResponse>(content);

            return weather!.Days.FirstOrDefault()!;
        }
        catch (Exception ex)
        {
            return new Weather(ex.Message, "Error", "Error");
        }
    }

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

            var distance = respnse?.rows![0]!.elements![0]!.distance!.text;
            var duration = respnse?.rows![0]!.elements![0]!.duration!.text;
            return new { Destination = destination, Distance = distance, Duration = duration };
        }
        catch (Exception)
        {
            return new { Error = "Error occurred! Please check your input" };
        }
    }

    public async Task<Dictionary<string, object>> GetExchangeRates(Currency code)
    {
        if (code is Currency.SEK) return null!;

        await UpdateExchangeRateRecords();
        return await GetSelectedCurrency(code);
    }

    private async Task<Dictionary<string, object>> GetSelectedCurrency(Currency code)
    {
        var targetProperty = new ExchangeRate().GetType().GetProperty(code.ToString());
        var rates = await _dbContext.ExchangeRate.ToListAsync();
        //Remove RemoveAll after October
        rates.RemoveAll(rate => DateTime.Parse(rate.Date) > DateTime.Now);

        var values = rates.Select(rate => new
        {
            Date = rate.Date,
            Value = targetProperty!.GetValue(rate)
        }).ToDictionary(date => date.Date, date => date.Value);

        return values!;
    }

    private async Task UpdateExchangeRateRecords()
    {
        var rateDates = await _dbContext.ExchangeRate.Select(rates => rates.Date).ToListAsync();
        var orderDates = await CheckOrderDates();
        foreach (var orderDate in orderDates)
        {
            if (rateDates.Exists(rateDate => rateDate == orderDate) == false)
            {
                _dbContext.ExchangeRate.Add(new ExchangeRate { Date = orderDate });
            }
        }
        await _dbContext.SaveChangesAsync();
        await UpdateRatesIfNull();
    }

    private async Task<List<string>> CheckOrderDates()
    {
        List<string> exchangeRateRecords = new List<string>();
        var orders = await _dbContext.Order.Select(order => order.OrderPlaced).Distinct()
            .OrderBy(date => date).ToListAsync();

        return orders;
    }

    private async Task UpdateRatesIfNull()
    {
        var exchangeRates = await _dbContext.ExchangeRate.ToListAsync();
        //Remove RemoveAll after October
        exchangeRates.RemoveAll(rate => DateTime.Parse(rate.Date) > DateTime.Now);
        foreach (var rate in exchangeRates)
        {
            if (rate.USD == 0) rate.USD = await RequestOldExchangeRate(Currency.USD, rate.Date);
            if (rate.EUR == 0) rate.EUR = await RequestOldExchangeRate(Currency.EUR, rate.Date);
            if (rate.GBP == 0) rate.GBP = await RequestOldExchangeRate(Currency.GBP, rate.Date);
            if (rate.CHF == 0) rate.CHF = await RequestOldExchangeRate(Currency.CHF, rate.Date);
            if (rate.JPY == 0) rate.JPY = await RequestOldExchangeRate(Currency.JPY, rate.Date);
            if (rate.CAD == 0) rate.CAD = await RequestOldExchangeRate(Currency.CAD, rate.Date);
            if (rate.NOK == 0) rate.NOK = await RequestOldExchangeRate(Currency.NOK, rate.Date);
            if (rate.DKK == 0) rate.DKK = await RequestOldExchangeRate(Currency.DKK, rate.Date);
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task<decimal> RequestOldExchangeRate(Currency code, string date)
    {
        try
        {
            string apiLocation = _config["CurrencyExchangeApi:Location"];
            string key = _config["CurrencyExchangeApi:Key"];
            apiLocation = apiLocation.Replace("[Currency]", code.ToString());
            apiLocation = apiLocation.Replace("[Date]", date);
            apiLocation += key;

            using HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(apiLocation);
            int startIndex = response.IndexOf(date) + 12;
            decimal value = decimal.Parse(response.Substring(startIndex).Replace("}", ""));

            return value;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public async Task<decimal> RequestLiveExchangeRate(Currency code)
    {
        try
        {
            var date = DateTime.Now.ToShortDateString();
            string apiLocation = _config["CurrencyExchangeApi:Location"];
            string key = _config["CurrencyExchangeApi:Key"];
            apiLocation = apiLocation.Replace("[Currency]", code.ToString());
            apiLocation = apiLocation.Replace("[Date]", date);
            apiLocation += key;

            using HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(apiLocation);
            int startIndex = response.IndexOf(date) + 12;
            decimal value = decimal.Parse(response.Substring(startIndex).Replace("}", ""));

            return value;
        }
        catch (Exception)
        {
            return 0;
        }
    }

}
