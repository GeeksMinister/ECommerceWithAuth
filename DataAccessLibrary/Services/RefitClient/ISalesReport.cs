namespace DataAccessLibrary.Services.RefitClient;

public interface ISalesReport
{
    [Get("/Order/SalesSummary")]
    Task<List<SalesSummary>> GetSalesSummary();

    [Get("/Order/TopSold")]
    Task<List<TopSoldProducts>> GetTopSoldProducts();

    [Get("/Order/ProductShortages")]
    Task<List<Product>> GetProductShortages();

    [Get("/Order/WeatherAndSalesRelation")]
    Task<List<SalesAndWeatherRelation>> GetSalesAndWeatherRelations();
}
