namespace ECommerceWithAuth.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
	private readonly IOrderRepository _orderRepository;
	private readonly IMapper _mapper;

	public OrderController(IOrderRepository orderRepository, IMapper mapper)
	{
		_orderRepository = orderRepository;
		_mapper = mapper;
	}

	[HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllOrders()
	{
        try
        {
            var result = await _orderRepository.GetAllOrders();
            if (result is null) return NotFound();
            var resultDto = _mapper.Map<List<OrderDto>>(result);
            Response.Headers.Append("Total_Orders", result.Count.ToString());
            return Ok(resultDto);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
	}

	[HttpGet("{guid}")]
	public async Task<IActionResult> GetOrderById(Guid guid)
	{
        try
        {
            var result = await _orderRepository.GetOrderById(guid);
            if (result is null) return NotFound();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
	}

	[HttpGet("SalesSummary")]
	public async Task<IActionResult> GetSalesSummary()
	{
		var result = await _orderRepository.GetSalesSummary();
        if (result is null) return Problem("No Data was Received From the Provider");
        return Ok(result);
	}

    [HttpGet("TopSold")]
    public async Task<IActionResult> GetTopSold()
    {
        var result = await _orderRepository.GetTopSold();
        if (result is null) return Problem("No Data was received From the Provider");
        return Ok(result);
    }

    [HttpGet("ProductShortages")]
    public async Task<IActionResult> GetProductShortages()
    {
        var result = await _orderRepository.GetProductShortages();
        if (result is null) return Problem("No Data was received From the Provider");
        Response.Headers.Append("Total-Products", result.Count.ToString());
        return Ok(result);
    }

    [HttpGet("WeatherAndSalesRelation")]
	public async Task<IActionResult> WeatherAndSalesRelation()
	{
		var result = await _orderRepository.GetWeatherAndSalesRelation();
        if (result is null) return Problem("No Data was received From the Provider");
        return Ok(result);
	}

    [HttpGet("DistanceMatrix/{destination}")]
    public async Task<IActionResult> GetDistance(string destination = "London")
    {
		try
		{
            var result = await _orderRepository.GetDistance(destination);
            if (result is null) return Problem("No Data was received From the Provider");
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    [HttpGet("ExchangeRates/{currency}")]
    public async Task<IActionResult> GetExchangeRates(Currency currency)
    {
        try
        {
            if (currency is Currency.SEK) return Ok(1);
            var result =  await _orderRepository.GetExchangeRates(currency);
            if (result is null) return Problem("No Data was received From the Provider");
            Response.Headers.Append("Total-dates-Requested", result.Count.ToString());
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    [HttpGet("RequestExchangeRate/{currency}")]
    public async Task<IActionResult> RequestExchangeRate(Currency currency)
    {
        try
        {
            if (currency == Currency.SEK) return Ok(1);
            var result =  await _orderRepository.RequestLiveExchangeRate(currency);
            if (result == 0) return Problem("No Data was received From the Provider");

            return Ok(result);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}
