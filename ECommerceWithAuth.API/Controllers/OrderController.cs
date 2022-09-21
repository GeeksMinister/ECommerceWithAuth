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
	public async Task<IActionResult> GetAllOrders()
	{
		var result = await _orderRepository.GetAllOrders();
		var resultDto = _mapper.Map<List<OrderDto>>(result);
		Response.Headers.Append("Total_Orders", result.Count.ToString());
		return Ok(resultDto);
	}

	[HttpGet("SellsSummary")]
	public async Task<IActionResult> GetSellsSummary()
	{
		var result = await _orderRepository.GetSellsSummary();
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
		var resultDto = _mapper.Map<List<ProductDto>>(result);
        return Ok(resultDto);
    }

    [HttpGet("WeatherAndSellsRelation")]
	public async Task<IActionResult> WeatherAndSellsRelation()
	{
		var result = await _orderRepository.GetWeatherAndSellsRelation();
        if (result is null) return Problem("No Data was received From the Provider");
        return Ok(result);
	}

    [HttpGet("DistanceMatrix")]
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

    [HttpGet("ExchangeRates")]
    public async Task<IActionResult> GetExchangeRates(Currency code)
    {
        try
        {
            var result =  await _orderRepository.GetExchangeRates(code);
            if (result is null) return Problem("No Data was received From the Provider");
            Response.Headers.Append("Total-dates-Requested", result.Count.ToString());
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}
