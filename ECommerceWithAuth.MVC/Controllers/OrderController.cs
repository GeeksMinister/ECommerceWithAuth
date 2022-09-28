using DataAccessLibrary.Models;

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

        decimal exchangeRate = 0;
        exchangeRate = await SetUpPrice(currency);
        if (exchangeRate > 0)  orders.ForEach(order => order.TotalToPay /= exchangeRate);
        if (exchangeRate == 0) currency = Currency.SEK;
        
        return View((orders, currency));
    }

    private async Task<decimal> SetUpPrice(Currency currency)
    {
        try
        {
            switch (currency)
            {
                case Currency.SEK: return 0;
                case Currency.USD: return await _productClientData.RequestExchangeRate(Currency.USD);
                case Currency.EUR: return await _productClientData.RequestExchangeRate(Currency.EUR);
                case Currency.GBP: return await _productClientData.RequestExchangeRate(Currency.GBP);
                case Currency.CAD: return await _productClientData.RequestExchangeRate(Currency.CAD);
                case Currency.CHF: return await _productClientData.RequestExchangeRate(Currency.CHF);
                case Currency.JPY: return await _productClientData.RequestExchangeRate(Currency.JPY);
                case Currency.NOK: return await _productClientData.RequestExchangeRate(Currency.NOK);
                case Currency.DKK: return await _productClientData.RequestExchangeRate(Currency.DKK);
                default: return 0;
            }
        }
        catch (Exception)
        {
            return 0;
        }
    }
}
