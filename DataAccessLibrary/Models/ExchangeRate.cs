namespace DataAccessLibrary.Models;

using System.Text.Json.Serialization;
public class ExchangeRate
{
    [Key]
    public int Id { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public string Date { get; set; } = DateTime.Now.ToString("yyy-MM-dd");

    [Column(TypeName = "money")]
    public decimal USD { get; set; }

    [Column(TypeName = "money")]
    public decimal EUR { get; set; }

    [Column(TypeName = "money")]
    public decimal GBP { get; set; }

    [Column(TypeName = "money")]
    public decimal CAD { get; set; }

    [Column(TypeName = "money")]
    public decimal CHF { get; set; }

    [Column(TypeName = "money")]
    public decimal JPY { get; set; }

    [Column(TypeName = "money")]
    public decimal NOK { get; set; }

    [Column(TypeName = "money")]
    public decimal DKK { get; set; }

    public ExchangeRate()
    {

    }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Currency
{
    SEK, USD, EUR, GBP, CAD, CHF, JPY, NOK, DKK
}
