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
		Response.Headers.Append("Total_Orders", result.Count.ToString());
		return Ok(result);
	}

	[HttpGet("SalesSummary")]
	public async Task<IActionResult> GetSalesSummary()
	{
		var result = await _orderRepository.GetSalesSummary();
		return Ok(result);
	}

	[HttpGet("TopSold")]
	public async Task<IActionResult> TopSold()
	{
		var result = await _orderRepository.GetSalesSummary();
		return Ok(result);
	}

}
