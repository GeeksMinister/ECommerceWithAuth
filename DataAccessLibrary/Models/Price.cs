namespace DataAccessLibrary.Models;

public class Price
{
    public Currency FromCurrency { get; set; } = Currency.SEK;
    public Currency ToCurrency { get; set; } = Currency.USD;
    public decimal Amount { get; set; } = 1;
    public string Result { get; set; } = string.Empty;
}