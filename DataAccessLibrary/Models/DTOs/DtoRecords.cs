namespace DataAccessLibrary.Models.DTOs;

public record PatchObj(string Path, string Value, string Op = "replace");

public record SalesAndWeatherRelation(string Date, int TotalOrders, Weather Weather);

public record Weather(string Temp, string Humidity, string Description);

public record WeatherApiResponse(Weather[] Days);

public record SalesSummary(string Date, decimal SalesSum, int TotalOrders, int TotalItems);

public record TopSoldProducts(string ProductId, string ProductName, int TotalSold);

