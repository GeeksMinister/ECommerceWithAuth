namespace ECommerceWithAuth.MVC.Controllers;

public class OrderController : Controller
{
    private readonly IOrderClientData _orderClientData;
    private readonly IProductClientData _productClientData;

    public OrderController(IOrderClientData orderClientData, IProductClientData productClientData)
    {
        _orderClientData = orderClientData;
        _productClientData = productClientData;
    }

    public async Task<IActionResult> Index(Currency currency)
    {
        var orders = await _orderClientData.GetAllOrder();
        if (orders == null) return NotFound();
        if (currency == Currency.SEK) return View((orders, currency));

        var exchangeRatesRecords = await _productClientData.ExchangeRates(currency);
        orders = ExchangePrices(orders, exchangeRatesRecords);

        return View((orders, currency));
    }

    private List<OrderDto> ExchangePrices(List<OrderDto> orders, Dictionary<string, decimal> records)
    {
        foreach (var order in orders)
        {
            if (records.ContainsKey(order.OrderPlaced))
            {
                var value = records.Where(rate => rate.Key == order.OrderPlaced).Select(rate => rate.Value).First();
                order.TotalToPay /= value;
            }
        }

        return orders;
    }

    public async Task<IActionResult> OrderItems(Guid id)
    {
        var order = await _orderClientData.GetOrderById(id);
        if (order == null) return NotFound();
        return View(order);

    }
}
